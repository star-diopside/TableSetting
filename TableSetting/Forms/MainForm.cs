using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using TableSetting.Models;
using TableSetting.Properties;

namespace TableSetting.Forms
{
    public partial class MainForm : Form
    {
        private DbProviderFactory? _factory;
        private DbDataAdapter? _adapter;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// アプリケーション全体の設定値を復元する
        /// </summary>
        /// <param name="settings">アプリケーションの設定値を表す ApplicationSettings クラスのインスタンス</param>
        private void RestoreApplicationSettings(ApplicationSettings settings)
        {
            comboDbProvider.SelectedValue = settings.DbProviderName;

            var listViewItems = from ConnectionSetting s in settings.ConnectionSettings
                                select new ListViewItem(new[] { s.Key, s.Value })
                                {
                                    Checked = s.Enable
                                };

            listViewConnectionString.Items.Clear();
            listViewConnectionString.Items.AddRange(listViewItems.ToArray());
        }

        /// <summary>
        /// アプリケーション全体の設定値を取得する
        /// </summary>
        /// <returns>アプリケーションの設定値を表す ApplicationSettings クラスのインスタンス</returns>
        private ApplicationSettings DumpApplicationSettings() => new ApplicationSettings
        {
            DbProviderName = (string)comboDbProvider.SelectedValue,
            ConnectionSettings = (from ListViewItem item in listViewConnectionString.Items
                                  select new ConnectionSetting
                                  {
                                      Key = item.SubItems[0].Text,
                                      Value = item.SubItems[1].Text,
                                      Enable = item.Checked
                                  }).ToList()
        };

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // コントロールの調整を行う
            SetupComponent();

            comboDbProvider.DataSource = DbProviderFactories.GetFactoryClasses()
                                                            .AsEnumerable()
                                                            .OrderBy(row => row.Field<string>("InvariantName"))
                                                            .AsDataView();
            comboDbProvider.DisplayMember = "InvariantName";
            comboDbProvider.ValueMember = "InvariantName";

            RestoreApplicationSettings(Settings.Default.ApplicationSettings);
        }

        /// <summary>
        /// コンポーネントの設定を行う
        /// </summary>
        private void SetupComponent()
        {
            // SQLパラメータ入力に関する設定を行う
            var columnName = new DataGridViewTextBoxColumn
            {
                Name = "name",
                HeaderText = "パラメータ",
                ValueType = typeof(string),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            var source = new BindingSource();
            foreach (var type in Enum.GetValues<DbType>())
            {
                source.Add(type);
            }

            var columnType = new DataGridViewComboBoxColumn
            {
                Name = "type",
                HeaderText = "型",
                ValueType = typeof(DbType),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DataSource = source
            };

            var columnValue = new DataGridViewTextBoxColumn
            {
                Name = "value",
                HeaderText = "値",
                ValueType = typeof(string),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };

            dataGridViewParameter.Columns.AddRange(columnName, columnType, columnValue);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Settings.Default.ApplicationSettings = DumpApplicationSettings();
            Settings.Default.Save();
        }

        /// <summary>
        /// フォームを閉じるイベント
        /// </summary>
        private void FormCloseEvent(object sender, EventArgs e) => Close();

        /// <summary>
        /// DataGridViewコントロールに行を追加したときに発生するイベント
        /// </summary>
        private void AddedRowsDataGridViewEvent(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ((DataGridView)sender).AutoResizeRow(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells);
        }

        /// <summary>
        /// SQL実行イベント
        /// </summary>
        private void ExecuteSqlEvent(object sender, EventArgs e)
        {
            var output = new StringBuilder();

            // カーソルを待機カーソルにする
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                _factory = DbProviderFactories.GetFactory((string)comboDbProvider.SelectedValue);

                output.AppendFormat("データベースプロバイダファクトリのクラス型: {0}", _factory.GetType())
                      .AppendLine()
                      .AppendLine();

                // 接続文字列を生成する
                var csb = _factory.CreateConnectionStringBuilder() ?? throw new NotImplementedException();

                output.AppendFormat("接続文字列ビルダのクラス型: {0}", csb.GetType())
                      .AppendLine();

                foreach (ListViewItem item in listViewConnectionString.CheckedItems)
                {
                    csb[item.Text] = item.SubItems[1].Text;
                }

                output.AppendFormat("接続文字列: {0}", csb.ConnectionString)
                      .AppendLine()
                      .AppendLine();

                // データベースからデータを取得する
                var conn = _factory.CreateConnection() ?? throw new NotImplementedException();
                var cmd = conn.CreateCommand();
                var dataTable = new DataTable();
                _adapter = _factory.CreateDataAdapter() ?? throw new NotImplementedException();

                output.AppendFormat("データベース接続オブジェクトのクラス型: {0}", conn.GetType())
                      .AppendLine()
                      .AppendFormat("データアダプタのクラス型: {0}", _adapter.GetType())
                      .AppendLine()
                      .AppendLine();

                conn.ConnectionString = csb.ConnectionString;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = textSql.Text;

                DbParameter ToDbParameter(DataGridViewRow row)
                {
                    var param = _factory.CreateParameter() ?? throw new NotImplementedException();
                    var type = row.Cells["type"].Value as DbType? ?? DbType.String;

                    param.ParameterName = (string)row.Cells["name"].Value;
                    param.DbType = type;
                    param.Value = DbTypeUtil.Parse((string)row.Cells["value"].Value, type);

                    return param;
                }

                var parameters = from DataGridViewRow row in dataGridViewParameter.Rows
                                 where !row.IsNewRow
                                 select ToDbParameter(row);
                cmd.Parameters.AddRange(parameters.ToArray());

                _adapter.SelectCommand = cmd;
                _adapter.Fill(dataTable);

                dataGridViewTable.Columns.Clear();
                dataGridViewTable.DataSource = dataTable;
                dataGridViewTable.DataMember = string.Empty;

                output.AppendFormat("[{0}] データ取得は正常に完了しました。", DateTime.Now)
                      .AppendLine();

                textOutput.Text = output.ToString();

                buttonRollback.Enabled = true;
                buttonCheckUpdateCommand.Enabled = true;
                buttonExecuteUpdate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                output.AppendLine("<<例外が発生する前に出力された文字列>>")
                      .AppendLine(ex.ToString());
                textOutput.Text = output.ToString();
                buttonCheckUpdateCommand.Enabled = false;
                buttonExecuteUpdate.Enabled = false;
            }
        }

        /// <summary>
        /// 更新SQLコマンド確認イベント
        /// </summary>
        private void CheckUpdateCommandEvent(object sender, EventArgs e)
        {
            if (_factory is null || _adapter is null)
            {
                throw new InvalidOperationException();
            }

            var output = new StringBuilder();

            // カーソルを待機カーソルにする
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                CreateUpdateCommand(_factory, _adapter, output);
                textOutput.Text = output.ToString();
            }
            catch (Exception ex)
            {
                var error = new StringBuilder();

                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.AppendLine(ex.ToString());

                if (output.Length > 0)
                {
                    error.AppendLine()
                         .AppendLine("<<例外が発生する前に出力された文字列>>")
                         .AppendLine(output.ToString());
                }

                textOutput.Text = error.ToString();
            }
        }

        /// <summary>
        /// 更新実行イベント
        /// </summary>
        private void ExecuteUpdateEvent(object sender, EventArgs e)
        {
            if (_factory is null || _adapter is null || _adapter.SelectCommand is null || _adapter.SelectCommand.Connection is null)
            {
                throw new InvalidOperationException();
            }

            var output = new StringBuilder();

            // カーソルを待機カーソルにする
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                DbConnection? conn = null;
                DbTransaction? tran = null;
                int countUpdate;

                try
                {
                    conn = _adapter.SelectCommand.Connection;
                    conn.Open();

                    tran = conn.BeginTransaction();
                    _adapter.SelectCommand.Transaction = tran;

                    CreateUpdateCommand(_factory, _adapter, output);

                    // データベースの更新を行う
                    countUpdate = _adapter.Update((DataTable)dataGridViewTable.DataSource);

                    // トランザクションをコミットする
                    tran.Commit();
                }
                catch (Exception)
                {
                    try
                    {
                        // トランザクションをロールバックする
                        tran?.Rollback();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    throw;
                }
                finally
                {
                    conn?.Close();
                }

                textOutput.Text = output.ToString();
                MessageBox.Show(this, countUpdate + " 件更新されました", "情報",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                var error = new StringBuilder();

                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.AppendLine(ex.ToString());

                if (output.Length > 0)
                {
                    error.AppendLine()
                         .AppendLine("<<例外が発生する前に出力された文字列>>")
                         .AppendLine(output.ToString());
                }

                textOutput.Text = error.ToString();
            }
        }

        /// <summary>
        /// 更新系SQLの自動生成を行う
        /// </summary>
        /// <param name="factory">データ取得時に指定されたデータプロバイダのDbProviderFactory</param>
        /// <param name="adapter">データ取得時に生成したDbDataAdapter</param>
        /// <param name="log">実行ログ出力先のStringBuilder</param>
        private static void CreateUpdateCommand(DbProviderFactory factory, DbDataAdapter adapter, StringBuilder log)
        {
            // CommandBuilderを生成し、DbDataAdapterに関連付ける
            var cb = factory.CreateCommandBuilder() ?? throw new NotImplementedException();
            cb.DataAdapter = adapter;

            log.AppendFormat("<<{0} クラスのオブジェクトによって動的に生成された SQL>>", cb.GetType())
               .AppendLine();

            // INSERT文を生成する
            log.AppendLine("INSERT SQL コマンド:")
               .AppendLine(cb.GetInsertCommand().CommandText)
               .AppendLine()
               .AppendLine("INSERT SQL コマンド パラメータ:");
            foreach (DbParameter param in cb.GetInsertCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn)
                   .AppendLine();
            }
            log.AppendLine();

            // UPDATE文を生成する
            log.AppendLine("UPDATE SQL コマンド:")
               .AppendLine(cb.GetUpdateCommand().CommandText)
               .AppendLine()
               .AppendLine("UPDATE SQL コマンド パラメータ:");
            foreach (DbParameter param in cb.GetUpdateCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn)
                   .AppendLine();
            }
            log.AppendLine();

            // DELETE文を生成する
            log.AppendLine("DELETE SQL コマンド:")
               .AppendLine(cb.GetDeleteCommand().CommandText)
               .AppendLine()
               .AppendLine("DELETE SQL コマンド パラメータ:");
            foreach (DbParameter param in cb.GetDeleteCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn)
                   .AppendLine();
            }
        }

        /// <summary>
        /// 接続文字列設定値追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionItemAddEvent(object sender, EventArgs e)
        {
            using var dialog = new EditConnectionStringDialog
            {
                Text = "項目の追加"
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ListViewItem listItem = listViewConnectionString.Items.Add(dialog.Key);
                listItem.SubItems.Add(dialog.Value);
                listItem.Checked = dialog.EnableItem;
            }
        }

        /// <summary>
        /// 接続文字列設定値編集イベント
        /// </summary>
        private void ConnectionItemEditEvent(object sender, EventArgs e)
        {
            if (listViewConnectionString.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "編集する項目を選択してください。", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem listItem = listViewConnectionString.SelectedItems[0];
            using var dialog = new EditConnectionStringDialog
            {
                Text = "項目の編集",
                Key = listItem.Text,
                Value = listItem.SubItems[1].Text,
                EnableItem = listItem.Checked
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                listItem.Text = dialog.Key;
                listItem.SubItems[1].Text = dialog.Value;
                listItem.Checked = dialog.EnableItem;
            }
        }

        /// <summary>
        /// 接続文字列設定値削除イベント
        /// </summary>
        private void ConnectionItemDeleteEvent(object sender, EventArgs e)
        {
            if (listViewConnectionString.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "削除する項目を選択してください。", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(this, "選択された項目を削除してもよろしいですか？", "確認",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listViewConnectionString.Items.Remove(listViewConnectionString.SelectedItems[0]);
            }
        }

        /// <summary>
        /// 接続文字列設定値の設定コントロール選択変更時のイベント
        /// </summary>
        private void listViewConnectionString_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool state = listViewConnectionString.SelectedIndices.Count != 0;
            buttonEdit.Enabled = buttonDelete.Enabled = state;
        }

        /// <summary>
        /// 設定ファイルを読み込むイベント
        /// </summary>
        private void OpenSettingEvent(object sender, EventArgs e)
        {
            try
            {
                using var dialog = new OpenFileDialog
                {
                    Filter = "設定ファイル (*.dat)|*.dat|すべてのファイル (*.*)|*.*"
                };

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    using var stream = new GZipStream(new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read), CompressionMode.Decompress);
                    var serializer = new XmlSerializer(typeof(ApplicationSettings));

                    if (serializer.Deserialize(stream) is ApplicationSettings settings)
                    {
                        RestoreApplicationSettings(settings);
                    }
                    else
                    {
                        throw new InvalidOperationException("ファイルフォーマットが不正です。");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 設定ファイルを保存するイベント
        /// </summary>
        private void SaveSettingEvent(object sender, EventArgs e)
        {
            try
            {
                using var dialog = new SaveFileDialog
                {
                    Filter = "設定ファイル (*.dat)|*.dat|すべてのファイル (*.*)|*.*"
                };

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    using var stream = new GZipStream(new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write), CompressionMode.Compress);
                    var settrings = DumpApplicationSettings();
                    var serializer = new XmlSerializer(settrings.GetType());
                    serializer.Serialize(stream, settrings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ログ出力をファイルに保存するイベント
        /// </summary>
        private void SaveOutputLogEvent(object sender, EventArgs e)
        {
            try
            {
                using var dialog = new SaveFileDialog
                {
                    Filter = "ログ ファイル (*.log)|*.log|すべてのファイル (*.*)|*.*"
                };

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    using var writer = new StreamWriter(dialog.FileName, false, Encoding.Default);
                    writer.Write(textOutput.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ローカルでの変更をロールバックする。
        /// </summary>
        private void RollbackChangesEvent(object sender, EventArgs e)
        {
            if (dataGridViewTable.DataSource is DataTable dataTable)
            {
                dataTable.RejectChanges();
            }
        }
    }
}
