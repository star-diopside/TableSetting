namespace TableSetting.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listViewConnectionString = new System.Windows.Forms.ListView();
            this.headerKey = new System.Windows.Forms.ColumnHeader();
            this.headerValue = new System.Windows.Forms.ColumnHeader();
            this.buttonExecuteSql = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelDbProvider = new System.Windows.Forms.Label();
            this.comboDbProvider = new System.Windows.Forms.ComboBox();
            this.labelSql = new System.Windows.Forms.Label();
            this.textSql = new System.Windows.Forms.TextBox();
            this.splitSettings = new System.Windows.Forms.SplitContainer();
            this.panelConnectionStringEdit = new System.Windows.Forms.Panel();
            this.flowConnectionStringEditButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.splitExecuteSql = new System.Windows.Forms.SplitContainer();
            this.panelSqlParameter = new System.Windows.Forms.Panel();
            this.dataGridViewParameter = new System.Windows.Forms.DataGridView();
            this.labelParameter = new System.Windows.Forms.Label();
            this.flowExecuteButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitBottom = new System.Windows.Forms.SplitContainer();
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.flowUpdateButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonExecuteUpdate = new System.Windows.Forms.Button();
            this.buttonCheckUpdateCommand = new System.Windows.Forms.Button();
            this.buttonRollback = new System.Windows.Forms.Button();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpenSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileSaveSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSaveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.splitSettings.Panel1.SuspendLayout();
            this.splitSettings.Panel2.SuspendLayout();
            this.splitSettings.SuspendLayout();
            this.panelConnectionStringEdit.SuspendLayout();
            this.flowConnectionStringEditButtonPanel.SuspendLayout();
            this.splitExecuteSql.Panel1.SuspendLayout();
            this.splitExecuteSql.Panel2.SuspendLayout();
            this.splitExecuteSql.SuspendLayout();
            this.panelSqlParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParameter)).BeginInit();
            this.flowExecuteButtonPanel.SuspendLayout();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.splitBottom.Panel1.SuspendLayout();
            this.splitBottom.Panel2.SuspendLayout();
            this.splitBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            this.flowUpdateButtonPanel.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewConnectionString
            // 
            this.listViewConnectionString.CheckBoxes = true;
            this.listViewConnectionString.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerKey,
            this.headerValue});
            this.listViewConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewConnectionString.FullRowSelect = true;
            this.listViewConnectionString.HideSelection = false;
            this.listViewConnectionString.Location = new System.Drawing.Point(0, 0);
            this.listViewConnectionString.MultiSelect = false;
            this.listViewConnectionString.Name = "listViewConnectionString";
            this.listViewConnectionString.Size = new System.Drawing.Size(320, 157);
            this.listViewConnectionString.TabIndex = 0;
            this.listViewConnectionString.UseCompatibleStateImageBehavior = false;
            this.listViewConnectionString.View = System.Windows.Forms.View.Details;
            this.listViewConnectionString.SelectedIndexChanged += new System.EventHandler(this.listViewConnectionString_SelectedIndexChanged);
            // 
            // headerKey
            // 
            this.headerKey.Text = "キー";
            this.headerKey.Width = 136;
            // 
            // headerValue
            // 
            this.headerValue.Text = "値";
            this.headerValue.Width = 136;
            // 
            // buttonExecuteSql
            // 
            this.buttonExecuteSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExecuteSql.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonExecuteSql.Location = new System.Drawing.Point(6, 94);
            this.buttonExecuteSql.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.buttonExecuteSql.Name = "buttonExecuteSql";
            this.buttonExecuteSql.Size = new System.Drawing.Size(75, 23);
            this.buttonExecuteSql.TabIndex = 0;
            this.buttonExecuteSql.Text = "実行";
            this.buttonExecuteSql.UseVisualStyleBackColor = true;
            this.buttonExecuteSql.Click += new System.EventHandler(this.ExecuteSqlEvent);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonAdd.Location = new System.Drawing.Point(6, 0);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(64, 23);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "追加";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ConnectionItemAddEvent);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Enabled = false;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonEdit.Location = new System.Drawing.Point(6, 29);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(64, 23);
            this.buttonEdit.TabIndex = 1;
            this.buttonEdit.Text = "編集";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ConnectionItemEditEvent);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Enabled = false;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDelete.Location = new System.Drawing.Point(6, 58);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(64, 23);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "削除";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ConnectionItemDeleteEvent);
            // 
            // labelDbProvider
            // 
            this.labelDbProvider.AutoSize = true;
            this.labelDbProvider.Location = new System.Drawing.Point(0, 6);
            this.labelDbProvider.Name = "labelDbProvider";
            this.labelDbProvider.Size = new System.Drawing.Size(97, 12);
            this.labelDbProvider.TabIndex = 0;
            this.labelDbProvider.Text = "データプロバイダ(&D):";
            // 
            // comboDbProvider
            // 
            this.comboDbProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDbProvider.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboDbProvider.FormattingEnabled = true;
            this.comboDbProvider.Location = new System.Drawing.Point(103, 3);
            this.comboDbProvider.Name = "comboDbProvider";
            this.comboDbProvider.Size = new System.Drawing.Size(287, 20);
            this.comboDbProvider.TabIndex = 1;
            // 
            // labelSql
            // 
            this.labelSql.AutoSize = true;
            this.labelSql.Location = new System.Drawing.Point(3, 6);
            this.labelSql.Name = "labelSql";
            this.labelSql.Size = new System.Drawing.Size(67, 12);
            this.labelSql.TabIndex = 0;
            this.labelSql.Text = "実行SQL(&S):";
            // 
            // textSql
            // 
            this.textSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textSql.Location = new System.Drawing.Point(3, 21);
            this.textSql.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.textSql.Multiline = true;
            this.textSql.Name = "textSql";
            this.textSql.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textSql.Size = new System.Drawing.Size(342, 44);
            this.textSql.TabIndex = 1;
            this.textSql.Text = "SELECT * FROM info";
            // 
            // splitSettings
            // 
            this.splitSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSettings.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitSettings.Location = new System.Drawing.Point(0, 0);
            this.splitSettings.Name = "splitSettings";
            // 
            // splitSettings.Panel1
            // 
            this.splitSettings.Panel1.Controls.Add(this.panelConnectionStringEdit);
            this.splitSettings.Panel1.Controls.Add(this.comboDbProvider);
            this.splitSettings.Panel1.Controls.Add(this.labelDbProvider);
            // 
            // splitSettings.Panel2
            // 
            this.splitSettings.Panel2.Controls.Add(this.splitExecuteSql);
            this.splitSettings.Size = new System.Drawing.Size(745, 189);
            this.splitSettings.SplitterDistance = 396;
            this.splitSettings.TabIndex = 0;
            // 
            // panelConnectionStringEdit
            // 
            this.panelConnectionStringEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelConnectionStringEdit.Controls.Add(this.listViewConnectionString);
            this.panelConnectionStringEdit.Controls.Add(this.flowConnectionStringEditButtonPanel);
            this.panelConnectionStringEdit.Location = new System.Drawing.Point(0, 29);
            this.panelConnectionStringEdit.Name = "panelConnectionStringEdit";
            this.panelConnectionStringEdit.Size = new System.Drawing.Size(393, 157);
            this.panelConnectionStringEdit.TabIndex = 2;
            // 
            // flowConnectionStringEditButtonPanel
            // 
            this.flowConnectionStringEditButtonPanel.AutoSize = true;
            this.flowConnectionStringEditButtonPanel.Controls.Add(this.buttonAdd);
            this.flowConnectionStringEditButtonPanel.Controls.Add(this.buttonEdit);
            this.flowConnectionStringEditButtonPanel.Controls.Add(this.buttonDelete);
            this.flowConnectionStringEditButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowConnectionStringEditButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowConnectionStringEditButtonPanel.Location = new System.Drawing.Point(320, 0);
            this.flowConnectionStringEditButtonPanel.Name = "flowConnectionStringEditButtonPanel";
            this.flowConnectionStringEditButtonPanel.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.flowConnectionStringEditButtonPanel.Size = new System.Drawing.Size(73, 157);
            this.flowConnectionStringEditButtonPanel.TabIndex = 1;
            // 
            // splitExecuteSql
            // 
            this.splitExecuteSql.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitExecuteSql.Location = new System.Drawing.Point(0, 0);
            this.splitExecuteSql.Name = "splitExecuteSql";
            this.splitExecuteSql.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitExecuteSql.Panel1
            // 
            this.splitExecuteSql.Panel1.Controls.Add(this.labelSql);
            this.splitExecuteSql.Panel1.Controls.Add(this.textSql);
            // 
            // splitExecuteSql.Panel2
            // 
            this.splitExecuteSql.Panel2.Controls.Add(this.panelSqlParameter);
            this.splitExecuteSql.Panel2.Controls.Add(this.flowExecuteButtonPanel);
            this.splitExecuteSql.Size = new System.Drawing.Size(345, 189);
            this.splitExecuteSql.SplitterDistance = 68;
            this.splitExecuteSql.TabIndex = 0;
            // 
            // panelSqlParameter
            // 
            this.panelSqlParameter.Controls.Add(this.dataGridViewParameter);
            this.panelSqlParameter.Controls.Add(this.labelParameter);
            this.panelSqlParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSqlParameter.Location = new System.Drawing.Point(0, 0);
            this.panelSqlParameter.Name = "panelSqlParameter";
            this.panelSqlParameter.Size = new System.Drawing.Size(264, 117);
            this.panelSqlParameter.TabIndex = 0;
            // 
            // dataGridViewParameter
            // 
            this.dataGridViewParameter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewParameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParameter.Location = new System.Drawing.Point(3, 15);
            this.dataGridViewParameter.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.dataGridViewParameter.Name = "dataGridViewParameter";
            this.dataGridViewParameter.RowTemplate.Height = 21;
            this.dataGridViewParameter.Size = new System.Drawing.Size(261, 102);
            this.dataGridViewParameter.TabIndex = 1;
            this.dataGridViewParameter.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.AddedRowsDataGridViewEvent);
            // 
            // labelParameter
            // 
            this.labelParameter.AutoSize = true;
            this.labelParameter.Location = new System.Drawing.Point(3, 0);
            this.labelParameter.Name = "labelParameter";
            this.labelParameter.Size = new System.Drawing.Size(85, 12);
            this.labelParameter.TabIndex = 0;
            this.labelParameter.Text = "SQLパラメータ(&P)";
            // 
            // flowExecuteButtonPanel
            // 
            this.flowExecuteButtonPanel.AutoSize = true;
            this.flowExecuteButtonPanel.Controls.Add(this.buttonExecuteSql);
            this.flowExecuteButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowExecuteButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowExecuteButtonPanel.Location = new System.Drawing.Point(264, 0);
            this.flowExecuteButtonPanel.Name = "flowExecuteButtonPanel";
            this.flowExecuteButtonPanel.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.flowExecuteButtonPanel.Size = new System.Drawing.Size(81, 117);
            this.flowExecuteButtonPanel.TabIndex = 1;
            // 
            // splitMain
            // 
            this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMain.Location = new System.Drawing.Point(12, 27);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.splitSettings);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitBottom);
            this.splitMain.Size = new System.Drawing.Size(745, 418);
            this.splitMain.SplitterDistance = 189;
            this.splitMain.TabIndex = 1;
            // 
            // splitBottom
            // 
            this.splitBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBottom.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitBottom.Location = new System.Drawing.Point(0, 0);
            this.splitBottom.Name = "splitBottom";
            this.splitBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitBottom.Panel1
            // 
            this.splitBottom.Panel1.Controls.Add(this.dataGridViewTable);
            this.splitBottom.Panel1.Controls.Add(this.flowUpdateButtonPanel);
            this.splitBottom.Panel1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            // 
            // splitBottom.Panel2
            // 
            this.splitBottom.Panel2.Controls.Add(this.textOutput);
            this.splitBottom.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.splitBottom.Size = new System.Drawing.Size(745, 225);
            this.splitBottom.SplitterDistance = 136;
            this.splitBottom.TabIndex = 0;
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTable.Location = new System.Drawing.Point(0, 3);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.RowTemplate.Height = 21;
            this.dataGridViewTable.Size = new System.Drawing.Size(745, 101);
            this.dataGridViewTable.TabIndex = 0;
            // 
            // flowUpdateButtonPanel
            // 
            this.flowUpdateButtonPanel.AutoSize = true;
            this.flowUpdateButtonPanel.Controls.Add(this.buttonExecuteUpdate);
            this.flowUpdateButtonPanel.Controls.Add(this.buttonCheckUpdateCommand);
            this.flowUpdateButtonPanel.Controls.Add(this.buttonRollback);
            this.flowUpdateButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowUpdateButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowUpdateButtonPanel.Location = new System.Drawing.Point(0, 104);
            this.flowUpdateButtonPanel.Name = "flowUpdateButtonPanel";
            this.flowUpdateButtonPanel.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.flowUpdateButtonPanel.Size = new System.Drawing.Size(745, 32);
            this.flowUpdateButtonPanel.TabIndex = 1;
            // 
            // buttonExecuteUpdate
            // 
            this.buttonExecuteUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExecuteUpdate.Enabled = false;
            this.buttonExecuteUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonExecuteUpdate.Location = new System.Drawing.Point(617, 6);
            this.buttonExecuteUpdate.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.buttonExecuteUpdate.Name = "buttonExecuteUpdate";
            this.buttonExecuteUpdate.Size = new System.Drawing.Size(128, 23);
            this.buttonExecuteUpdate.TabIndex = 2;
            this.buttonExecuteUpdate.Text = "更新処理を行う";
            this.buttonExecuteUpdate.UseVisualStyleBackColor = true;
            this.buttonExecuteUpdate.Click += new System.EventHandler(this.ExecuteUpdateEvent);
            // 
            // buttonCheckUpdateCommand
            // 
            this.buttonCheckUpdateCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheckUpdateCommand.Enabled = false;
            this.buttonCheckUpdateCommand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCheckUpdateCommand.Location = new System.Drawing.Point(483, 6);
            this.buttonCheckUpdateCommand.Name = "buttonCheckUpdateCommand";
            this.buttonCheckUpdateCommand.Size = new System.Drawing.Size(128, 23);
            this.buttonCheckUpdateCommand.TabIndex = 1;
            this.buttonCheckUpdateCommand.Text = "更新SQLの確認";
            this.buttonCheckUpdateCommand.UseVisualStyleBackColor = true;
            this.buttonCheckUpdateCommand.Click += new System.EventHandler(this.CheckUpdateCommandEvent);
            // 
            // buttonRollback
            // 
            this.buttonRollback.Enabled = false;
            this.buttonRollback.Location = new System.Drawing.Point(317, 6);
            this.buttonRollback.Name = "buttonRollback";
            this.buttonRollback.Size = new System.Drawing.Size(160, 23);
            this.buttonRollback.TabIndex = 0;
            this.buttonRollback.Text = "変更をロールバックする";
            this.buttonRollback.UseVisualStyleBackColor = true;
            this.buttonRollback.Click += new System.EventHandler(this.RollbackChangesEvent);
            // 
            // textOutput
            // 
            this.textOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textOutput.Location = new System.Drawing.Point(0, 3);
            this.textOutput.Multiline = true;
            this.textOutput.Name = "textOutput";
            this.textOutput.ReadOnly = true;
            this.textOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textOutput.Size = new System.Drawing.Size(745, 82);
            this.textOutput.TabIndex = 0;
            this.textOutput.WordWrap = false;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(769, 26);
            this.menuStripMain.TabIndex = 0;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileOpenSetting,
            this.menuFileSeparator1,
            this.menuFileSaveSetting,
            this.menuFileSaveLog,
            this.menuFileSeparator2,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(85, 22);
            this.menuFile.Text = "ファイル(&F)";
            // 
            // menuFileOpenSetting
            // 
            this.menuFileOpenSetting.Image = ((System.Drawing.Image)(resources.GetObject("menuFileOpenSetting.Image")));
            this.menuFileOpenSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuFileOpenSetting.Name = "menuFileOpenSetting";
            this.menuFileOpenSetting.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuFileOpenSetting.Size = new System.Drawing.Size(189, 22);
            this.menuFileOpenSetting.Text = "開く(&O)";
            this.menuFileOpenSetting.Click += new System.EventHandler(this.OpenSettingEvent);
            // 
            // menuFileSeparator1
            // 
            this.menuFileSeparator1.Name = "menuFileSeparator1";
            this.menuFileSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // menuFileSaveSetting
            // 
            this.menuFileSaveSetting.Image = ((System.Drawing.Image)(resources.GetObject("menuFileSaveSetting.Image")));
            this.menuFileSaveSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuFileSaveSetting.Name = "menuFileSaveSetting";
            this.menuFileSaveSetting.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuFileSaveSetting.Size = new System.Drawing.Size(189, 22);
            this.menuFileSaveSetting.Text = "保存(&S)...";
            this.menuFileSaveSetting.Click += new System.EventHandler(this.SaveSettingEvent);
            // 
            // menuFileSaveLog
            // 
            this.menuFileSaveLog.Name = "menuFileSaveLog";
            this.menuFileSaveLog.Size = new System.Drawing.Size(189, 22);
            this.menuFileSaveLog.Text = "ログ出力の保存(&L)...";
            this.menuFileSaveLog.Click += new System.EventHandler(this.SaveOutputLogEvent);
            // 
            // menuFileSeparator2
            // 
            this.menuFileSeparator2.Name = "menuFileSeparator2";
            this.menuFileSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(189, 22);
            this.menuFileExit.Text = "終了(&X)";
            this.menuFileExit.Click += new System.EventHandler(this.FormCloseEvent);
            // 
            // MainForm
            // 
            this.AcceptButton = this.buttonExecuteSql;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 457);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "テーブルの設定";
            this.splitSettings.Panel1.ResumeLayout(false);
            this.splitSettings.Panel1.PerformLayout();
            this.splitSettings.Panel2.ResumeLayout(false);
            this.splitSettings.ResumeLayout(false);
            this.panelConnectionStringEdit.ResumeLayout(false);
            this.panelConnectionStringEdit.PerformLayout();
            this.flowConnectionStringEditButtonPanel.ResumeLayout(false);
            this.splitExecuteSql.Panel1.ResumeLayout(false);
            this.splitExecuteSql.Panel1.PerformLayout();
            this.splitExecuteSql.Panel2.ResumeLayout(false);
            this.splitExecuteSql.Panel2.PerformLayout();
            this.splitExecuteSql.ResumeLayout(false);
            this.panelSqlParameter.ResumeLayout(false);
            this.panelSqlParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParameter)).EndInit();
            this.flowExecuteButtonPanel.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.ResumeLayout(false);
            this.splitBottom.Panel1.ResumeLayout(false);
            this.splitBottom.Panel1.PerformLayout();
            this.splitBottom.Panel2.ResumeLayout(false);
            this.splitBottom.Panel2.PerformLayout();
            this.splitBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            this.flowUpdateButtonPanel.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewConnectionString;
        private System.Windows.Forms.ColumnHeader headerKey;
        private System.Windows.Forms.ColumnHeader headerValue;
        private System.Windows.Forms.Button buttonExecuteSql;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelDbProvider;
        private System.Windows.Forms.ComboBox comboDbProvider;
        private System.Windows.Forms.Label labelSql;
        private System.Windows.Forms.TextBox textSql;
        private System.Windows.Forms.SplitContainer splitSettings;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.SplitContainer splitBottom;
        private System.Windows.Forms.Button buttonExecuteUpdate;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.Button buttonCheckUpdateCommand;
        private System.Windows.Forms.FlowLayoutPanel flowUpdateButtonPanel;
        private System.Windows.Forms.FlowLayoutPanel flowExecuteButtonPanel;
        private System.Windows.Forms.Panel panelConnectionStringEdit;
        private System.Windows.Forms.FlowLayoutPanel flowConnectionStringEditButtonPanel;
        private System.Windows.Forms.Button buttonRollback;
        private System.Windows.Forms.DataGridView dataGridViewParameter;
        private System.Windows.Forms.SplitContainer splitExecuteSql;
        private System.Windows.Forms.Label labelParameter;
        private System.Windows.Forms.Panel panelSqlParameter;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveSetting;
        private System.Windows.Forms.ToolStripMenuItem menuFileSaveLog;
        private System.Windows.Forms.ToolStripSeparator menuFileSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpenSetting;
        private System.Windows.Forms.ToolStripSeparator menuFileSeparator1;
    }
}

