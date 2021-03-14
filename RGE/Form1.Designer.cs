
namespace RGE
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ComboBox comboBox_LogLevel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.openFileDialogPCList = new System.Windows.Forms.OpenFileDialog();
            this.tabTask = new System.Windows.Forms.TabPage();
            this.bPCSave = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bPCLoad = new System.Windows.Forms.Button();
            this.tTimeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tThisPC = new System.Windows.Forms.TextBox();
            this.tPCMaskSelect = new System.Windows.Forms.TextBox();
            this.bPCSelectOnMask = new System.Windows.Forms.Button();
            this.bPCUnSelectAll = new System.Windows.Forms.Button();
            this.bPCSelectAll = new System.Windows.Forms.Button();
            this.bAddthisPC = new System.Windows.Forms.Button();
            this.bDeletePCSelected = new System.Windows.Forms.Button();
            this.bAddPCFromDomain = new System.Windows.Forms.Button();
            this.chkList_PC = new System.Windows.Forms.CheckedListBox();
            this.tabCommandControl = new System.Windows.Forms.TabControl();
            this.tabCommandCopyFileFolder = new System.Windows.Forms.TabPage();
            this.chkCopyOnlyNewer = new System.Windows.Forms.CheckBox();
            this.chkCopyOverride = new System.Windows.Forms.CheckBox();
            this.bSourceCopy = new System.Windows.Forms.Button();
            this.bTargetCopy = new System.Windows.Forms.Button();
            this.tTargetCopy = new System.Windows.Forms.TextBox();
            this.tSourceCopy = new System.Windows.Forms.TextBox();
            this.tabComandScript = new System.Windows.Forms.TabPage();
            this.tScriptFile = new System.Windows.Forms.TextBox();
            this.tabCommandCommand = new System.Windows.Forms.TabPage();
            this.tCommand = new System.Windows.Forms.TextBox();
            this.lCommand = new System.Windows.Forms.Label();
            this.tabCommandInstall = new System.Windows.Forms.TabPage();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.wResult = new System.Windows.Forms.WebBrowser();
            this.tabMainControl = new System.Windows.Forms.TabControl();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.process1 = new System.Diagnostics.Process();
            this.tool = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolbGo = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            comboBox_LogLevel = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.tabTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabCommandControl.SuspendLayout();
            this.tabCommandCopyFileFolder.SuspendLayout();
            this.tabComandScript.SuspendLayout();
            this.tabCommandCommand.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tabMainControl.SuspendLayout();
            this.tool.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_LogLevel
            // 
            comboBox_LogLevel.Enabled = false;
            comboBox_LogLevel.Items.AddRange(new object[] {
            "Info",
            "Sucess",
            "Errors",
            "Debug"});
            comboBox_LogLevel.Location = new System.Drawing.Point(887, 236);
            comboBox_LogLevel.Name = "comboBox_LogLevel";
            comboBox_LogLevel.Size = new System.Drawing.Size(209, 30);
            comboBox_LogLevel.TabIndex = 9;
            comboBox_LogLevel.Text = "Info";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 739);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1184, 23);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 15);
            // 
            // openFileDialogPCList
            // 
            this.openFileDialogPCList.DefaultExt = "txt";
            this.openFileDialogPCList.FileName = "openFileDialogPCList";
            this.openFileDialogPCList.Filter = "Text|*.txt|CSV|*.csv|All|*.*";
            // 
            // tabTask
            // 
            this.tabTask.Controls.Add(this.bPCSave);
            this.tabTask.Controls.Add(this.pictureBox1);
            this.tabTask.Controls.Add(this.bPCLoad);
            this.tabTask.Controls.Add(this.tTimeout);
            this.tabTask.Controls.Add(this.label2);
            this.tabTask.Controls.Add(this.label1);
            this.tabTask.Controls.Add(comboBox_LogLevel);
            this.tabTask.Controls.Add(this.tThisPC);
            this.tabTask.Controls.Add(this.tPCMaskSelect);
            this.tabTask.Controls.Add(this.bPCSelectOnMask);
            this.tabTask.Controls.Add(this.bPCUnSelectAll);
            this.tabTask.Controls.Add(this.bPCSelectAll);
            this.tabTask.Controls.Add(this.bAddthisPC);
            this.tabTask.Controls.Add(this.bDeletePCSelected);
            this.tabTask.Controls.Add(this.bAddPCFromDomain);
            this.tabTask.Controls.Add(this.chkList_PC);
            this.tabTask.Controls.Add(this.tabCommandControl);
            this.tabTask.Location = new System.Drawing.Point(4, 31);
            this.tabTask.Name = "tabTask";
            this.tabTask.Padding = new System.Windows.Forms.Padding(3);
            this.tabTask.Size = new System.Drawing.Size(1176, 704);
            this.tabTask.TabIndex = 0;
            this.tabTask.Text = "Task";
            this.tabTask.UseVisualStyleBackColor = true;
            // 
            // bPCSave
            // 
            this.bPCSave.BackColor = System.Drawing.Color.Transparent;
            this.bPCSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bPCSave.FlatAppearance.BorderSize = 0;
            this.bPCSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPCSave.Image = ((System.Drawing.Image)(resources.GetObject("bPCSave.Image")));
            this.bPCSave.Location = new System.Drawing.Point(36, 6);
            this.bPCSave.Name = "bPCSave";
            this.bPCSave.Size = new System.Drawing.Size(24, 24);
            this.bPCSave.TabIndex = 17;
            this.toolTip1.SetToolTip(this.bPCSave, "Save the list of hosts to file");
            this.bPCSave.UseVisualStyleBackColor = false;
            this.bPCSave.Click += new System.EventHandler(this.bPCSave_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1064, 380);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 116);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // bPCLoad
            // 
            this.bPCLoad.BackColor = System.Drawing.Color.Transparent;
            this.bPCLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bPCLoad.FlatAppearance.BorderSize = 0;
            this.bPCLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPCLoad.Image = ((System.Drawing.Image)(resources.GetObject("bPCLoad.Image")));
            this.bPCLoad.Location = new System.Drawing.Point(6, 6);
            this.bPCLoad.Name = "bPCLoad";
            this.bPCLoad.Size = new System.Drawing.Size(24, 24);
            this.bPCLoad.TabIndex = 15;
            this.toolTip1.SetToolTip(this.bPCLoad, "Open file with list of hosts");
            this.bPCLoad.UseVisualStyleBackColor = false;
            this.bPCLoad.Click += new System.EventHandler(this.bPCLoad_Click);
            // 
            // tTimeout
            // 
            this.tTimeout.Enabled = false;
            this.tTimeout.Location = new System.Drawing.Point(975, 281);
            this.tTimeout.Name = "tTimeout";
            this.tTimeout.Size = new System.Drawing.Size(121, 28);
            this.tTimeout.TabIndex = 14;
            this.tTimeout.Text = "60";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(883, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "Timeout";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(883, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Log level";
            // 
            // tThisPC
            // 
            this.tThisPC.Location = new System.Drawing.Point(887, 92);
            this.tThisPC.Name = "tThisPC";
            this.tThisPC.Size = new System.Drawing.Size(129, 28);
            this.tThisPC.TabIndex = 8;
            this.tThisPC.Text = "pc100";
            // 
            // tPCMaskSelect
            // 
            this.tPCMaskSelect.Enabled = false;
            this.tPCMaskSelect.Location = new System.Drawing.Point(887, 138);
            this.tPCMaskSelect.Name = "tPCMaskSelect";
            this.tPCMaskSelect.Size = new System.Drawing.Size(129, 28);
            this.tPCMaskSelect.TabIndex = 8;
            this.tPCMaskSelect.Text = "*";
            // 
            // bPCSelectOnMask
            // 
            this.bPCSelectOnMask.Enabled = false;
            this.bPCSelectOnMask.Location = new System.Drawing.Point(1022, 132);
            this.bPCSelectOnMask.Name = "bPCSelectOnMask";
            this.bPCSelectOnMask.Size = new System.Drawing.Size(142, 34);
            this.bPCSelectOnMask.TabIndex = 7;
            this.bPCSelectOnMask.Text = "Select suitable";
            this.bPCSelectOnMask.UseVisualStyleBackColor = true;
            // 
            // bPCUnSelectAll
            // 
            this.bPCUnSelectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bPCUnSelectAll.FlatAppearance.BorderSize = 0;
            this.bPCUnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPCUnSelectAll.Image = global::RGE.Properties.Resources.UnSelectAll;
            this.bPCUnSelectAll.Location = new System.Drawing.Point(96, 6);
            this.bPCUnSelectAll.Name = "bPCUnSelectAll";
            this.bPCUnSelectAll.Size = new System.Drawing.Size(24, 24);
            this.bPCUnSelectAll.TabIndex = 7;
            this.toolTip1.SetToolTip(this.bPCUnSelectAll, "Select none");
            this.bPCUnSelectAll.UseVisualStyleBackColor = true;
            this.bPCUnSelectAll.Click += new System.EventHandler(this.bPCUnSelectAll_Click);
            // 
            // bPCSelectAll
            // 
            this.bPCSelectAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bPCSelectAll.FlatAppearance.BorderSize = 0;
            this.bPCSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPCSelectAll.Image = global::RGE.Properties.Resources.SelectAll;
            this.bPCSelectAll.Location = new System.Drawing.Point(66, 6);
            this.bPCSelectAll.Name = "bPCSelectAll";
            this.bPCSelectAll.Size = new System.Drawing.Size(24, 24);
            this.bPCSelectAll.TabIndex = 7;
            this.toolTip1.SetToolTip(this.bPCSelectAll, "Select All");
            this.bPCSelectAll.UseVisualStyleBackColor = true;
            this.bPCSelectAll.Click += new System.EventHandler(this.bPCSelectAll_Click);
            // 
            // bAddthisPC
            // 
            this.bAddthisPC.Location = new System.Drawing.Point(1022, 92);
            this.bAddthisPC.Name = "bAddthisPC";
            this.bAddthisPC.Size = new System.Drawing.Size(74, 34);
            this.bAddthisPC.TabIndex = 6;
            this.bAddthisPC.Text = "Add this";
            this.bAddthisPC.UseVisualStyleBackColor = true;
            this.bAddthisPC.Click += new System.EventHandler(this.bAddthisPC_Click);
            // 
            // bDeletePCSelected
            // 
            this.bDeletePCSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bDeletePCSelected.FlatAppearance.BorderSize = 0;
            this.bDeletePCSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDeletePCSelected.Image = global::RGE.Properties.Resources.Delete;
            this.bDeletePCSelected.Location = new System.Drawing.Point(126, 6);
            this.bDeletePCSelected.Name = "bDeletePCSelected";
            this.bDeletePCSelected.Size = new System.Drawing.Size(24, 24);
            this.bDeletePCSelected.TabIndex = 6;
            this.toolTip1.SetToolTip(this.bDeletePCSelected, "Delete selected hosts");
            this.bDeletePCSelected.UseVisualStyleBackColor = true;
            this.bDeletePCSelected.Click += new System.EventHandler(this.bDeletePCSelected_Click);
            // 
            // bAddPCFromDomain
            // 
            this.bAddPCFromDomain.Enabled = false;
            this.bAddPCFromDomain.Location = new System.Drawing.Point(887, 172);
            this.bAddPCFromDomain.Name = "bAddPCFromDomain";
            this.bAddPCFromDomain.Size = new System.Drawing.Size(209, 34);
            this.bAddPCFromDomain.TabIndex = 6;
            this.bAddPCFromDomain.Text = "Add From Domain";
            this.bAddPCFromDomain.UseVisualStyleBackColor = true;
            this.bAddPCFromDomain.Click += new System.EventHandler(this.bAddPCFromDomain_Click);
            // 
            // chkList_PC
            // 
            this.chkList_PC.CheckOnClick = true;
            this.chkList_PC.FormattingEnabled = true;
            this.chkList_PC.HorizontalScrollbar = true;
            this.chkList_PC.Location = new System.Drawing.Point(6, 35);
            this.chkList_PC.MultiColumn = true;
            this.chkList_PC.Name = "chkList_PC";
            this.chkList_PC.Size = new System.Drawing.Size(816, 441);
            this.chkList_PC.TabIndex = 5;
            // 
            // tabCommandControl
            // 
            this.tabCommandControl.Controls.Add(this.tabCommandCopyFileFolder);
            this.tabCommandControl.Controls.Add(this.tabComandScript);
            this.tabCommandControl.Controls.Add(this.tabCommandCommand);
            this.tabCommandControl.Controls.Add(this.tabCommandInstall);
            this.tabCommandControl.Location = new System.Drawing.Point(-4, 487);
            this.tabCommandControl.Name = "tabCommandControl";
            this.tabCommandControl.SelectedIndex = 0;
            this.tabCommandControl.Size = new System.Drawing.Size(1172, 217);
            this.tabCommandControl.TabIndex = 4;
            this.tabCommandControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabCommandControl_DrawItem);
            // 
            // tabCommandCopyFileFolder
            // 
            this.tabCommandCopyFileFolder.Controls.Add(this.chkCopyOnlyNewer);
            this.tabCommandCopyFileFolder.Controls.Add(this.chkCopyOverride);
            this.tabCommandCopyFileFolder.Controls.Add(this.bSourceCopy);
            this.tabCommandCopyFileFolder.Controls.Add(this.bTargetCopy);
            this.tabCommandCopyFileFolder.Controls.Add(this.tTargetCopy);
            this.tabCommandCopyFileFolder.Controls.Add(this.tSourceCopy);
            this.tabCommandCopyFileFolder.Location = new System.Drawing.Point(4, 31);
            this.tabCommandCopyFileFolder.Name = "tabCommandCopyFileFolder";
            this.tabCommandCopyFileFolder.Padding = new System.Windows.Forms.Padding(3);
            this.tabCommandCopyFileFolder.Size = new System.Drawing.Size(1164, 182);
            this.tabCommandCopyFileFolder.TabIndex = 0;
            this.tabCommandCopyFileFolder.Text = "Copy Folder";
            this.tabCommandCopyFileFolder.UseVisualStyleBackColor = true;
            // 
            // chkCopyOnlyNewer
            // 
            this.chkCopyOnlyNewer.AutoSize = true;
            this.chkCopyOnlyNewer.Checked = true;
            this.chkCopyOnlyNewer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopyOnlyNewer.Enabled = false;
            this.chkCopyOnlyNewer.Location = new System.Drawing.Point(283, 12);
            this.chkCopyOnlyNewer.Name = "chkCopyOnlyNewer";
            this.chkCopyOnlyNewer.Size = new System.Drawing.Size(129, 28);
            this.chkCopyOnlyNewer.TabIndex = 3;
            this.chkCopyOnlyNewer.Text = "Only newer";
            this.chkCopyOnlyNewer.UseVisualStyleBackColor = true;
            // 
            // chkCopyOverride
            // 
            this.chkCopyOverride.AutoSize = true;
            this.chkCopyOverride.Checked = true;
            this.chkCopyOverride.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCopyOverride.Enabled = false;
            this.chkCopyOverride.Location = new System.Drawing.Point(149, 12);
            this.chkCopyOverride.Name = "chkCopyOverride";
            this.chkCopyOverride.Size = new System.Drawing.Size(105, 28);
            this.chkCopyOverride.TabIndex = 3;
            this.chkCopyOverride.Text = "Override";
            this.chkCopyOverride.UseVisualStyleBackColor = true;
            this.chkCopyOverride.Validated += new System.EventHandler(this.chkCopyOverride_Validated);
            // 
            // bSourceCopy
            // 
            this.bSourceCopy.Location = new System.Drawing.Point(13, 10);
            this.bSourceCopy.Name = "bSourceCopy";
            this.bSourceCopy.Size = new System.Drawing.Size(100, 30);
            this.bSourceCopy.TabIndex = 2;
            this.bSourceCopy.Text = "Source...";
            this.bSourceCopy.UseVisualStyleBackColor = true;
            this.bSourceCopy.Click += new System.EventHandler(this.bSourceCopy_Click);
            // 
            // bTargetCopy
            // 
            this.bTargetCopy.Location = new System.Drawing.Point(13, 93);
            this.bTargetCopy.Name = "bTargetCopy";
            this.bTargetCopy.Size = new System.Drawing.Size(100, 30);
            this.bTargetCopy.TabIndex = 2;
            this.bTargetCopy.Text = "Target...";
            this.bTargetCopy.UseVisualStyleBackColor = true;
            this.bTargetCopy.Click += new System.EventHandler(this.bTargetCopy_Click);
            // 
            // tTargetCopy
            // 
            this.tTargetCopy.Location = new System.Drawing.Point(15, 129);
            this.tTargetCopy.Name = "tTargetCopy";
            this.tTargetCopy.Size = new System.Drawing.Size(1117, 28);
            this.tTargetCopy.TabIndex = 1;
            // 
            // tSourceCopy
            // 
            this.tSourceCopy.Location = new System.Drawing.Point(13, 46);
            this.tSourceCopy.Name = "tSourceCopy";
            this.tSourceCopy.Size = new System.Drawing.Size(1117, 28);
            this.tSourceCopy.TabIndex = 1;
            // 
            // tabComandScript
            // 
            this.tabComandScript.Controls.Add(this.tScriptFile);
            this.tabComandScript.Location = new System.Drawing.Point(4, 31);
            this.tabComandScript.Name = "tabComandScript";
            this.tabComandScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabComandScript.Size = new System.Drawing.Size(1164, 182);
            this.tabComandScript.TabIndex = 1;
            this.tabComandScript.Text = "Script";
            this.tabComandScript.UseVisualStyleBackColor = true;
            this.tabComandScript.Enter += new System.EventHandler(this.tabComandScript_Click);
            // 
            // tScriptFile
            // 
            this.tScriptFile.Location = new System.Drawing.Point(8, 6);
            this.tScriptFile.Name = "tScriptFile";
            this.tScriptFile.Size = new System.Drawing.Size(1117, 28);
            this.tScriptFile.TabIndex = 2;
            this.tScriptFile.Text = "xcopy01.cmd";
            // 
            // tabCommandCommand
            // 
            this.tabCommandCommand.Controls.Add(this.tCommand);
            this.tabCommandCommand.Controls.Add(this.lCommand);
            this.tabCommandCommand.Location = new System.Drawing.Point(4, 31);
            this.tabCommandCommand.Name = "tabCommandCommand";
            this.tabCommandCommand.Padding = new System.Windows.Forms.Padding(3);
            this.tabCommandCommand.Size = new System.Drawing.Size(1164, 182);
            this.tabCommandCommand.TabIndex = 2;
            this.tabCommandCommand.Text = "Command";
            this.tabCommandCommand.UseVisualStyleBackColor = true;
            this.tabCommandCommand.Enter += new System.EventHandler(this.tabComandScript_Click);
            // 
            // tCommand
            // 
            this.tCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tCommand.Location = new System.Drawing.Point(173, 306);
            this.tCommand.Multiline = true;
            this.tCommand.Name = "tCommand";
            this.tCommand.Size = new System.Drawing.Size(926, 81);
            this.tCommand.TabIndex = 4;
            this.tCommand.Text = "dir";
            // 
            // lCommand
            // 
            this.lCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lCommand.AutoSize = true;
            this.lCommand.Location = new System.Drawing.Point(66, 332);
            this.lCommand.Name = "lCommand";
            this.lCommand.Size = new System.Drawing.Size(98, 24);
            this.lCommand.TabIndex = 3;
            this.lCommand.Text = "Command";
            this.lCommand.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabCommandInstall
            // 
            this.tabCommandInstall.Location = new System.Drawing.Point(4, 31);
            this.tabCommandInstall.Name = "tabCommandInstall";
            this.tabCommandInstall.Padding = new System.Windows.Forms.Padding(3);
            this.tabCommandInstall.Size = new System.Drawing.Size(1164, 182);
            this.tabCommandInstall.TabIndex = 3;
            this.tabCommandInstall.Text = "Install";
            this.tabCommandInstall.UseVisualStyleBackColor = true;
            this.tabCommandInstall.Enter += new System.EventHandler(this.tabComandScript_Click);
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.wResult);
            this.tabResult.Location = new System.Drawing.Point(4, 31);
            this.tabResult.Name = "tabResult";
            this.tabResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabResult.Size = new System.Drawing.Size(1176, 704);
            this.tabResult.TabIndex = 1;
            this.tabResult.Text = " Result ";
            this.tabResult.UseVisualStyleBackColor = true;
            // 
            // wResult
            // 
            this.wResult.AllowWebBrowserDrop = false;
            this.wResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wResult.Location = new System.Drawing.Point(3, 3);
            this.wResult.MinimumSize = new System.Drawing.Size(20, 20);
            this.wResult.Name = "wResult";
            this.wResult.ScriptErrorsSuppressed = true;
            this.wResult.Size = new System.Drawing.Size(1170, 698);
            this.wResult.TabIndex = 1;
            // 
            // tabMainControl
            // 
            this.tabMainControl.Controls.Add(this.tabTask);
            this.tabMainControl.Controls.Add(this.tabResult);
            this.tabMainControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMainControl.Location = new System.Drawing.Point(0, 0);
            this.tabMainControl.Name = "tabMainControl";
            this.tabMainControl.SelectedIndex = 0;
            this.tabMainControl.Size = new System.Drawing.Size(1184, 739);
            this.tabMainControl.TabIndex = 7;
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // tool
            // 
            this.tool.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.ToolbGo});
            this.tool.Location = new System.Drawing.Point(0, 0);
            this.tool.Name = "tool";
            this.tool.Size = new System.Drawing.Size(1184, 31);
            this.tool.TabIndex = 8;
            this.tool.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(40, 28);
            this.toolStripButton1.Text = "Task";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(53, 28);
            this.toolStripButton2.Text = "Result";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // ToolbGo
            // 
            this.ToolbGo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolbGo.BackColor = System.Drawing.Color.Lime;
            this.ToolbGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ToolbGo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ToolbGo.Image = ((System.Drawing.Image)(resources.GetObject("ToolbGo.Image")));
            this.ToolbGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolbGo.Name = "ToolbGo";
            this.ToolbGo.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ToolbGo.Size = new System.Drawing.Size(93, 28);
            this.ToolbGo.Text = "     Go     ";
            this.ToolbGo.Click += new System.EventHandler(this.ToolbGo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.tool);
            this.Controls.Add(this.tabMainControl);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multi Synchro File Copier";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabTask.ResumeLayout(false);
            this.tabTask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabCommandControl.ResumeLayout(false);
            this.tabCommandCopyFileFolder.ResumeLayout(false);
            this.tabCommandCopyFileFolder.PerformLayout();
            this.tabComandScript.ResumeLayout(false);
            this.tabComandScript.PerformLayout();
            this.tabCommandCommand.ResumeLayout(false);
            this.tabCommandCommand.PerformLayout();
            this.tabResult.ResumeLayout(false);
            this.tabMainControl.ResumeLayout(false);
            this.tool.ResumeLayout(false);
            this.tool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialogPCList;
        private System.Windows.Forms.TabPage tabTask;
        private System.Windows.Forms.TextBox tPCMaskSelect;
        private System.Windows.Forms.Button bPCSelectOnMask;
        private System.Windows.Forms.Button bPCUnSelectAll;
        private System.Windows.Forms.Button bPCSelectAll;
        public System.Windows.Forms.CheckedListBox chkList_PC;
        private System.Windows.Forms.TabControl tabCommandControl;
        private System.Windows.Forms.TabPage tabCommandCopyFileFolder;
        private System.Windows.Forms.TabPage tabComandScript;
        private System.Windows.Forms.TabPage tabCommandCommand;
        private System.Windows.Forms.TextBox tCommand;
        private System.Windows.Forms.Label lCommand;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.TabControl tabMainControl;
        private System.Windows.Forms.TextBox tThisPC;
        private System.Windows.Forms.Button bAddthisPC;
        private System.Windows.Forms.Button bAddPCFromDomain;
        private System.Windows.Forms.Button bDeletePCSelected;
        private System.Windows.Forms.TextBox tTargetCopy;
        private System.Windows.Forms.TextBox tSourceCopy;
        private System.Windows.Forms.Button bTargetCopy;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button bSourceCopy;
        private System.Windows.Forms.CheckBox chkCopyOnlyNewer;
        private System.Windows.Forms.CheckBox chkCopyOverride;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.ToolStrip tool;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton ToolbGo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TabPage tabCommandInstall;
        public System.Windows.Forms.WebBrowser wResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tScriptFile;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button bPCLoad;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button bPCSave;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

