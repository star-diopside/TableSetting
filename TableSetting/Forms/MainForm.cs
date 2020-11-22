using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using TableSetting.Models;

namespace TableSetting.Forms
{
    public partial class MainForm : Form
    {
        private DbProviderFactory _factory;
        private DbDataAdapter _adapter;

        public MainForm()
        {
            InitializeComponent();

            // フォントを設定する
            this.Font = SystemInformation.MenuFont;
        }

        /// <summary>
        /// アプリケーション全体の設定値を復元する
        /// </summary>
        /// <param name="settings">アプリケーションの設定値を表す ApplicationSettings クラスのインスタンス</param>
        private void RestoreApplicationSettings(ApplicationSettings settings)
        {
            comboDbProvider.SelectedValue = settings.DbProviderName;

            listViewConnectionString.Items.Clear();
            foreach (ConnectionSetting connSetting in settings.ConnectionSettings)
            {
                ListViewItem item = new ListViewItem();

                item.Text = connSetting.Key;
                item.SubItems.Add(connSetting.Value);
                item.Checked = connSetting.Enable;

                listViewConnectionString.Items.Add(item);
            }
        }

        /// <summary>
        /// アプリケーション全体の設定値を取得する
        /// </summary>
        /// <returns>アプリケーションの設定値を表す ApplicationSettings クラスのインスタンス</returns>
        private ApplicationSettings DumpApplicationSettings()
        {
            ApplicationSettings appSettings = new ApplicationSettings();

            appSettings.DbProviderName = (string)comboDbProvider.SelectedValue;
            appSettings.ConnectionSettings = new List<ConnectionSetting>();

            foreach (ListViewItem item in listViewConnectionString.Items)
            {
                ConnectionSetting connSetting = new ConnectionSetting();

                connSetting.Key = item.SubItems[0].Text;
                connSetting.Value = item.SubItems[1].Text;
                connSetting.Enable = item.Checked;

                appSettings.ConnectionSettings.Add(connSetting);
            }

            return appSettings;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // コントロールの調整を行う
            SetupComponent();

            comboDbProvider.DataSource = DbProviderFactories.GetFactoryClasses();
            comboDbProvider.DisplayMember = "Name";
            comboDbProvider.ValueMember = "AssemblyQualifiedName";

            RestoreApplicationSettings(Properties.Settings.Default.ApplicationSettings);
        }

        /// <summary>
        /// コンポーネントの設定を行う
        /// </summary>
        private void SetupComponent()
        {
            // コントロールの位置を調整する
            comboDbProvider.Left = labelDbProvider.Right + 8;
            comboDbProvider.Width = comboDbProvider.Parent.Width - 9 - comboDbProvider.Left;
            labelDbProvider.Top = comboDbProvider.Top + (comboDbProvider.Height - labelDbProvider.Height) / 2;

            // SQLパラメータ入力に関する設定を行う
            DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn();
            DataGridViewComboBoxColumn columnType = new DataGridViewComboBoxColumn();
            DataGridViewTextBoxColumn columnValue = new DataGridViewTextBoxColumn();

            columnName.Name = "name";
            columnName.HeaderText = "パラメータ";
            columnName.ValueType = typeof(string);
            columnName.SortMode = DataGridViewColumnSortMode.NotSortable;
            columnName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            columnType.Name = "type";
            columnType.HeaderText = "型";
            columnType.ValueType = typeof(DbType);
            columnType.SortMode = DataGridViewColumnSortMode.NotSortable;
            columnType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            BindingSource source = new BindingSource();
            foreach (DbType type in Enum.GetValues(typeof(DbType)))
            {
                source.Add(type);
            }
            columnType.DataSource = source;

            columnValue.Name = "value";
            columnValue.HeaderText = "値";
            columnValue.ValueType = typeof(string);
            columnValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            columnValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridViewParameter.Columns.AddRange(columnName, columnType, columnValue);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Properties.Settings.Default.ApplicationSettings = DumpApplicationSettings();
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// フォームを閉じるイベント
        /// </summary>
        private void FormCloseEvent(object sender, EventArgs e)
        {
            Close();
        }

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
            StringBuilder output = new StringBuilder();

            // カーソルを待機カーソルにする
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                this._factory = DbProviderFactories.GetFactory(((DataRowView)comboDbProvider.SelectedItem).Row);

                output.AppendFormat("データベースプロバイダファクトリのクラス型: {0}", this._factory.GetType().ToString());
                output.AppendLine().AppendLine();


                // 接続文字列を生成する
                DbConnectionStringBuilder csb = this._factory.CreateConnectionStringBuilder();

                output.AppendFormat("接続文字列ビルダのクラス型: {0}", csb.GetType().ToString());
                output.AppendLine();

                foreach (ListViewItem item in listViewConnectionString.CheckedItems)
                {
                    csb[item.Text] = item.SubItems[1].Text;
                }

                output.AppendFormat("接続文字列: {0}", csb.ConnectionString);
                output.AppendLine().AppendLine();


                // データベースからデータを取得する
                DbConnection conn = this._factory.CreateConnection();
                DbCommand cmd = conn.CreateCommand();
                DataTable dataTable = new DataTable();
                this._adapter = this._factory.CreateDataAdapter();

                output.AppendFormat("データベース接続オブジェクトのクラス型: {0}", conn.GetType().ToString());
                output.AppendLine();
                output.AppendFormat("データアダプタのクラス型: {0}", this._adapter.GetType().ToString());
                output.AppendLine().AppendLine();

                conn.ConnectionString = csb.ConnectionString;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = textSql.Text;

                foreach (DataGridViewRow row in dataGridViewParameter.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        DbParameter param = this._factory.CreateParameter();
                        DbType type = (DbType)row.Cells["type"].Value;

                        param.ParameterName = (string)row.Cells["name"].Value;
                        param.DbType = type;
                        param.Value = DbTypeUtil.Parse((string)row.Cells["value"].Value, type);

                        cmd.Parameters.Add(param);
                    }
                }

                this._adapter.SelectCommand = cmd;
                this._adapter.Fill(dataTable);

                dataGridViewTable.Columns.Clear();
                dataGridViewTable.DataSource = dataTable;
                dataGridViewTable.DataMember = string.Empty;


                output.AppendFormat("< {0} > データ取得は正常に完了しました。", System.DateTime.Now);
                output.AppendLine();

                textOutput.Text = output.ToString();

                buttonRollback.Enabled = true;
                buttonCheckUpdateCommand.Enabled = true;
                buttonExecuteUpdate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                output.AppendLine();
                output.AppendLine(ex.ToString());
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
            StringBuilder output = new StringBuilder();

            // カーソルを待機カーソルにする
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                CreateUpdateCommand(this._factory, this._adapter, output);
                textOutput.Text = output.ToString();
            }
            catch (Exception ex)
            {
                StringBuilder error = new StringBuilder();

                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.AppendLine(ex.ToString());

                if (output.Length > 0)
                {
                    error.AppendLine().AppendLine();
                    error.AppendLine("<<<< 例外が発生する前に出力された文字列 >>>>");
                    error.AppendLine(output.ToString());
                }

                textOutput.Text = error.ToString();
            }
        }

        /// <summary>
        /// 更新実行イベント
        /// </summary>
        private void ExecuteUpdateEvent(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();

            // カーソルを待機カーソルにする
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                DbConnection conn = null;
                DbTransaction tran = null;
                int countUpdate = 0;

                try
                {
                    conn = this._adapter.SelectCommand.Connection;
                    conn.Open();

                    tran = conn.BeginTransaction();
                    this._adapter.SelectCommand.Transaction = tran;

                    CreateUpdateCommand(this._factory, this._adapter, output);

                    // データベースの更新を行う
                    countUpdate = this._adapter.Update((DataTable)dataGridViewTable.DataSource);

                    // トランザクションをコミットする
                    tran.Commit();
                }
                catch (Exception)
                {
                    if (tran != null)
                    {
                        try
                        {
                            // トランザクションをロールバックする
                            tran.Rollback();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    throw;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }

                textOutput.Text = output.ToString();
                MessageBox.Show(this, countUpdate.ToString() + " 件更新されました", "情報",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                StringBuilder error = new StringBuilder();

                MessageBox.Show(this, ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.AppendLine(ex.ToString());

                if (output.Length > 0)
                {
                    error.AppendLine().AppendLine();
                    error.AppendLine("<<<< 例外が発生する前に出力された文字列 >>>>");
                    error.AppendLine(output.ToString());
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
            DbCommandBuilder cb = factory.CreateCommandBuilder();
            cb.DataAdapter = adapter;

            log.AppendFormat("< {0} クラスのオブジェクトによって動的に生成された SQL >", cb.GetType().ToString());
            log.AppendLine();

            // INSERT文を生成する
            log.AppendLine("INSERT SQL コマンド:");
            log.AppendLine(cb.GetInsertCommand().CommandText);
            log.AppendLine();
            log.AppendLine("INSERT SQL コマンド パラメータ:");
            foreach (DbParameter param in cb.GetInsertCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn);
                log.AppendLine();
            }
            log.AppendLine().AppendLine();

            // UPDATE文を生成する
            log.AppendLine("UPDATE SQL コマンド:");
            log.AppendLine(cb.GetUpdateCommand().CommandText);
            log.AppendLine();
            log.AppendLine("UPDATE SQL コマンド パラメータ:");
            foreach (DbParameter param in cb.GetUpdateCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn);
                log.AppendLine();
            }
            log.AppendLine().AppendLine();

            // DELETE文を生成する
            log.AppendLine("DELETE SQL コマンド:");
            log.AppendLine(cb.GetDeleteCommand().CommandText);
            log.AppendLine();
            log.AppendLine("DELETE SQL コマンド パラメータ:");
            foreach (DbParameter param in cb.GetDeleteCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn);
                log.AppendLine();
            }
        }

        /// <summary>
        /// 接続文字列設定値追加イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionItemAddEvent(object sender, EventArgs e)
        {
            EditConnectionStringDialog dialog = new EditConnectionStringDialog();

            dialog.Text = "項目の追加";
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
            EditConnectionStringDialog dialog = new EditConnectionStringDialog();

            dialog.Text = "項目の編集";
            dialog.Key = listItem.Text;
            dialog.Value = listItem.SubItems[1].Text;
            dialog.EnableItem = listItem.Checked;
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
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Filter = "設定ファイル (*.dat)|*.dat|すべてのファイル (*.*)|*.*";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    using (Stream stream = new GZipStream(new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read), CompressionMode.Decompress))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
                        RestoreApplicationSettings((ApplicationSettings)serializer.Deserialize(stream));
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
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Filter = "設定ファイル (*.dat)|*.dat|すべてのファイル (*.*)|*.*";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    using (Stream stream = new GZipStream(new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write), CompressionMode.Compress))
                    {
                        ApplicationSettings settrings = DumpApplicationSettings();
                        XmlSerializer serializer = new XmlSerializer(settrings.GetType());
                        serializer.Serialize(stream, settrings);
                    }
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
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Filter = "ログ ファイル (*.log)|*.log|すべてのファイル (*.*)|*.*";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(dialog.FileName, false, Encoding.Default))
                    {
                        writer.Write(textOutput.Text);
                    }
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
            if (dataGridViewTable.DataSource != null)
            {
                ((DataTable)dataGridViewTable.DataSource).RejectChanges();
            }
        }
    }
}
