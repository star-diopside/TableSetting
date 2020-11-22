using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TableSetting
{
    public partial class MainForm : Form
    {
        private DbProviderFactory _factory;
        private DbDataAdapter _adapter;

        public MainForm()
        {
            InitializeComponent();

            // �t�H���g��ݒ肷��
            this.Font = SystemInformation.MenuFont;
        }

        /// <summary>
        /// �A�v���P�[�V�����S�̂̐ݒ�l�𕜌�����
        /// </summary>
        /// <param name="settings">�A�v���P�[�V�����̐ݒ�l��\�� ApplicationSettings �N���X�̃C���X�^���X</param>
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
        /// �A�v���P�[�V�����S�̂̐ݒ�l���擾����
        /// </summary>
        /// <returns>�A�v���P�[�V�����̐ݒ�l��\�� ApplicationSettings �N���X�̃C���X�^���X</returns>
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

            // �R���g���[���̒������s��
            SetupComponent();

            comboDbProvider.DataSource = DbProviderFactories.GetFactoryClasses();
            comboDbProvider.DisplayMember = "Name";
            comboDbProvider.ValueMember = "AssemblyQualifiedName";

            RestoreApplicationSettings(Properties.Settings.Default.ApplicationSettings);
        }

        /// <summary>
        /// �R���|�[�l���g�̐ݒ���s��
        /// </summary>
        private void SetupComponent()
        {
            // �R���g���[���̈ʒu�𒲐�����
            comboDbProvider.Left = labelDbProvider.Right + 8;
            comboDbProvider.Width = comboDbProvider.Parent.Width - 9 - comboDbProvider.Left;
            labelDbProvider.Top = comboDbProvider.Top + (comboDbProvider.Height - labelDbProvider.Height) / 2;

            // SQL�p�����[�^���͂Ɋւ���ݒ���s��
            DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn();
            DataGridViewComboBoxColumn columnType = new DataGridViewComboBoxColumn();
            DataGridViewTextBoxColumn columnValue = new DataGridViewTextBoxColumn();

            columnName.Name = "name";
            columnName.HeaderText = "�p�����[�^";
            columnName.ValueType = typeof(string);
            columnName.SortMode = DataGridViewColumnSortMode.NotSortable;
            columnName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            columnType.Name = "type";
            columnType.HeaderText = "�^";
            columnType.ValueType = typeof(DbType);
            columnType.SortMode = DataGridViewColumnSortMode.NotSortable;
            columnType.AutoSizeMode= DataGridViewAutoSizeColumnMode.Fill;

            BindingSource source = new BindingSource();
            foreach (DbType type in Enum.GetValues(typeof(DbType)))
            {
                source.Add(type);
            }
            columnType.DataSource = source;

            columnValue.Name = "value";
            columnValue.HeaderText = "�l";
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
        /// �t�H�[�������C�x���g
        /// </summary>
        private void FormCloseEvent(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// DataGridView�R���g���[���ɍs��ǉ������Ƃ��ɔ�������C�x���g
        /// </summary>
        private void AddedRowsDataGridViewEvent(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ((DataGridView)sender).AutoResizeRow(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells);
        }

        /// <summary>
        /// SQL���s�C�x���g
        /// </summary>
        private void ExecuteSqlEvent(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();

            // �J�[�\����ҋ@�J�[�\���ɂ���
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                this._factory = DbProviderFactories.GetFactory(((DataRowView)comboDbProvider.SelectedItem).Row);

                output.AppendFormat("�f�[�^�x�[�X�v���o�C�_�t�@�N�g���̃N���X�^: {0}", this._factory.GetType().ToString());
                output.AppendLine().AppendLine();


                // �ڑ�������𐶐�����
                DbConnectionStringBuilder csb = this._factory.CreateConnectionStringBuilder();

                output.AppendFormat("�ڑ�������r���_�̃N���X�^: {0}", csb.GetType().ToString());
                output.AppendLine();

                foreach (ListViewItem item in listViewConnectionString.CheckedItems)
                {
                    csb[item.Text] = item.SubItems[1].Text;
                }

                output.AppendFormat("�ڑ�������: {0}", csb.ConnectionString);
                output.AppendLine().AppendLine();


                // �f�[�^�x�[�X����f�[�^���擾����
                DbConnection conn = this._factory.CreateConnection();
                DbCommand cmd = conn.CreateCommand();
                DataTable dataTable = new DataTable();
                this._adapter = this._factory.CreateDataAdapter();

                output.AppendFormat("�f�[�^�x�[�X�ڑ��I�u�W�F�N�g�̃N���X�^: {0}", conn.GetType().ToString());
                output.AppendLine();
                output.AppendFormat("�f�[�^�A�_�v�^�̃N���X�^: {0}", this._adapter.GetType().ToString());
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


                output.AppendFormat("< {0} > �f�[�^�擾�͐���Ɋ������܂����B", System.DateTime.Now);
                output.AppendLine();

                textOutput.Text = output.ToString();

                buttonRollback.Enabled = true;
                buttonCheckUpdateCommand.Enabled = true;
                buttonExecuteUpdate.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                output.AppendLine();
                output.AppendLine(ex.ToString());
                textOutput.Text = output.ToString();
                buttonCheckUpdateCommand.Enabled = false;
                buttonExecuteUpdate.Enabled = false;
            }
        }

        /// <summary>
        /// �X�VSQL�R�}���h�m�F�C�x���g
        /// </summary>
        private void CheckUpdateCommandEvent(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();

            // �J�[�\����ҋ@�J�[�\���ɂ���
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                CreateUpdateCommand(this._factory, this._adapter, output);
                textOutput.Text = output.ToString();
            }
            catch (Exception ex)
            {
                StringBuilder error = new StringBuilder();

                MessageBox.Show(this, ex.Message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.AppendLine(ex.ToString());

                if (output.Length > 0)
                {
                    error.AppendLine().AppendLine();
                    error.AppendLine("<<<< ��O����������O�ɏo�͂��ꂽ������ >>>>");
                    error.AppendLine(output.ToString());
                }

                textOutput.Text = error.ToString();
            }
        }

        /// <summary>
        /// �X�V���s�C�x���g
        /// </summary>
        private void ExecuteUpdateEvent(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();

            // �J�[�\����ҋ@�J�[�\���ɂ���
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

                    // �f�[�^�x�[�X�̍X�V���s��
                    countUpdate = this._adapter.Update((DataTable)dataGridViewTable.DataSource);

                    // �g�����U�N�V�������R�~�b�g����
                    tran.Commit();
                }
                catch (Exception)
                {
                    if (tran != null)
                    {
                        try
                        {
                            // �g�����U�N�V���������[���o�b�N����
                            tran.Rollback();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, ex.Message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(this, countUpdate.ToString() + " ���X�V����܂���", "���",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                StringBuilder error = new StringBuilder();

                MessageBox.Show(this, ex.Message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error.AppendLine(ex.ToString());

                if (output.Length > 0)
                {
                    error.AppendLine().AppendLine();
                    error.AppendLine("<<<< ��O����������O�ɏo�͂��ꂽ������ >>>>");
                    error.AppendLine(output.ToString());
                }

                textOutput.Text = error.ToString();
            }
        }

        /// <summary>
        /// �X�V�nSQL�̎����������s��
        /// </summary>
        /// <param name="factory">�f�[�^�擾���Ɏw�肳�ꂽ�f�[�^�v���o�C�_��DbProviderFactory</param>
        /// <param name="adapter">�f�[�^�擾���ɐ�������DbDataAdapter</param>
        /// <param name="log">���s���O�o�͐��StringBuilder</param>
        private static void CreateUpdateCommand(DbProviderFactory factory, DbDataAdapter adapter, StringBuilder log)
        {
            // CommandBuilder�𐶐����ADbDataAdapter�Ɋ֘A�t����
            DbCommandBuilder cb = factory.CreateCommandBuilder();
            cb.DataAdapter = adapter;

            log.AppendFormat("< {0} �N���X�̃I�u�W�F�N�g�ɂ���ē��I�ɐ������ꂽ SQL >", cb.GetType().ToString());
            log.AppendLine();

            // INSERT���𐶐�����
            log.AppendLine("INSERT SQL �R�}���h:");
            log.AppendLine(cb.GetInsertCommand().CommandText);
            log.AppendLine();
            log.AppendLine("INSERT SQL �R�}���h �p�����[�^:");
            foreach (DbParameter param in cb.GetInsertCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn);
                log.AppendLine();
            }
            log.AppendLine().AppendLine();

            // UPDATE���𐶐�����
            log.AppendLine("UPDATE SQL �R�}���h:");
            log.AppendLine(cb.GetUpdateCommand().CommandText);
            log.AppendLine();
            log.AppendLine("UPDATE SQL �R�}���h �p�����[�^:");
            foreach (DbParameter param in cb.GetUpdateCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn);
                log.AppendLine();
            }
            log.AppendLine().AppendLine();

            // DELETE���𐶐�����
            log.AppendLine("DELETE SQL �R�}���h:");
            log.AppendLine(cb.GetDeleteCommand().CommandText);
            log.AppendLine();
            log.AppendLine("DELETE SQL �R�}���h �p�����[�^:");
            foreach (DbParameter param in cb.GetDeleteCommand().Parameters)
            {
                log.AppendFormat("{0} => DbType = {1}, SourceColumn = {2}", param.ParameterName, param.DbType, param.SourceColumn);
                log.AppendLine();
            }
        }

        /// <summary>
        /// �ڑ�������ݒ�l�ǉ��C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionItemAddEvent(object sender, EventArgs e)
        {
            EditConnectionStringDialog dialog = new EditConnectionStringDialog();

            dialog.Text = "���ڂ̒ǉ�";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                ListViewItem listItem = listViewConnectionString.Items.Add(dialog.Key);
                listItem.SubItems.Add(dialog.Value);
                listItem.Checked = dialog.EnableItem;
            }
        }

        /// <summary>
        /// �ڑ�������ݒ�l�ҏW�C�x���g
        /// </summary>
        private void ConnectionItemEditEvent(object sender, EventArgs e)
        {
            if (listViewConnectionString.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "�ҏW���鍀�ڂ�I�����Ă��������B", "�x��",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem listItem = listViewConnectionString.SelectedItems[0];
            EditConnectionStringDialog dialog = new EditConnectionStringDialog();

            dialog.Text = "���ڂ̕ҏW";
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
        /// �ڑ�������ݒ�l�폜�C�x���g
        /// </summary>
        private void ConnectionItemDeleteEvent(object sender, EventArgs e)
        {
            if (listViewConnectionString.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "�폜���鍀�ڂ�I�����Ă��������B", "�x��",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(this, "�I�����ꂽ���ڂ��폜���Ă���낵���ł����H", "�m�F",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listViewConnectionString.Items.Remove(listViewConnectionString.SelectedItems[0]);
            }
        }

        /// <summary>
        /// �ڑ�������ݒ�l�̐ݒ�R���g���[���I��ύX���̃C�x���g
        /// </summary>
        private void listViewConnectionString_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool state = listViewConnectionString.SelectedIndices.Count != 0;
            buttonEdit.Enabled = buttonDelete.Enabled = state;
        }

        /// <summary>
        /// �ݒ�t�@�C����ǂݍ��ރC�x���g
        /// </summary>
        private void OpenSettingEvent(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Filter = "�ݒ�t�@�C�� (*.dat)|*.dat|���ׂẴt�@�C�� (*.*)|*.*";

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
                MessageBox.Show(this, ex.Message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// �ݒ�t�@�C����ۑ�����C�x���g
        /// </summary>
        private void SaveSettingEvent(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Filter = "�ݒ�t�@�C�� (*.dat)|*.dat|���ׂẴt�@�C�� (*.*)|*.*";

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
                MessageBox.Show(this, ex.Message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ���O�o�͂��t�@�C���ɕۑ�����C�x���g
        /// </summary>
        private void SaveOutputLogEvent(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Filter = "���O �t�@�C�� (*.log)|*.log|���ׂẴt�@�C�� (*.*)|*.*";

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
                MessageBox.Show(this, ex.Message, "�G���[", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ���[�J���ł̕ύX�����[���o�b�N����B
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
