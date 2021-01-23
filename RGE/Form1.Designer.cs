
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabTask = new System.Windows.Forms.TabPage();
            this.tPCMaskSelect = new System.Windows.Forms.TextBox();
            this.bPCSelectOnMask = new System.Windows.Forms.Button();
            this.bPCUnSelectAll = new System.Windows.Forms.Button();
            this.bPCSelectAll = new System.Windows.Forms.Button();
            this.bRun = new System.Windows.Forms.Button();
            this.chkList_PC = new System.Windows.Forms.CheckedListBox();
            this.tabCommand = new System.Windows.Forms.TabControl();
            this.tabCopyFileFolder = new System.Windows.Forms.TabPage();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.tabTaskCommand = new System.Windows.Forms.TabPage();
            this.tCommand = new System.Windows.Forms.TextBox();
            this.lCommand = new System.Windows.Forms.Label();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.wResult = new System.Windows.Forms.WebBrowser();
            this.tabMainControl = new System.Windows.Forms.TabControl();
            this.bAddPCFromDomain = new System.Windows.Forms.Button();
            this.bAddthisPC = new System.Windows.Forms.Button();
            this.tThisPC = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tabTask.SuspendLayout();
            this.tabCommand.SuspendLayout();
            this.tabTaskCommand.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tabMainControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 740);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabTask
            // 
            this.tabTask.Controls.Add(this.tThisPC);
            this.tabTask.Controls.Add(this.tPCMaskSelect);
            this.tabTask.Controls.Add(this.bPCSelectOnMask);
            this.tabTask.Controls.Add(this.bPCUnSelectAll);
            this.tabTask.Controls.Add(this.bPCSelectAll);
            this.tabTask.Controls.Add(this.bAddthisPC);
            this.tabTask.Controls.Add(this.button3);
            this.tabTask.Controls.Add(this.bAddPCFromDomain);
            this.tabTask.Controls.Add(this.bRun);
            this.tabTask.Controls.Add(this.chkList_PC);
            this.tabTask.Controls.Add(this.tabCommand);
            this.tabTask.Location = new System.Drawing.Point(4, 26);
            this.tabTask.Name = "tabTask";
            this.tabTask.Padding = new System.Windows.Forms.Padding(3);
            this.tabTask.Size = new System.Drawing.Size(1176, 710);
            this.tabTask.TabIndex = 0;
            this.tabTask.Text = "Task";
            this.tabTask.UseVisualStyleBackColor = true;
            // 
            // tPCMaskSelect
            // 
            this.tPCMaskSelect.Enabled = false;
            this.tPCMaskSelect.Location = new System.Drawing.Point(192, 7);
            this.tPCMaskSelect.Name = "tPCMaskSelect";
            this.tPCMaskSelect.Size = new System.Drawing.Size(129, 24);
            this.tPCMaskSelect.TabIndex = 8;
            this.tPCMaskSelect.Text = "*";
            // 
            // bPCSelectOnMask
            // 
            this.bPCSelectOnMask.Enabled = false;
            this.bPCSelectOnMask.Location = new System.Drawing.Point(327, 6);
            this.bPCSelectOnMask.Name = "bPCSelectOnMask";
            this.bPCSelectOnMask.Size = new System.Drawing.Size(94, 34);
            this.bPCSelectOnMask.TabIndex = 7;
            this.bPCSelectOnMask.Text = "Select ";
            this.bPCSelectOnMask.UseVisualStyleBackColor = true;
            // 
            // bPCUnSelectAll
            // 
            this.bPCUnSelectAll.Location = new System.Drawing.Point(91, 6);
            this.bPCUnSelectAll.Name = "bPCUnSelectAll";
            this.bPCUnSelectAll.Size = new System.Drawing.Size(94, 34);
            this.bPCUnSelectAll.TabIndex = 7;
            this.bPCUnSelectAll.Text = "Unselect All";
            this.bPCUnSelectAll.UseVisualStyleBackColor = true;
            this.bPCUnSelectAll.Click += new System.EventHandler(this.bPCUnSelectAll_Click);
            // 
            // bPCSelectAll
            // 
            this.bPCSelectAll.Location = new System.Drawing.Point(6, 6);
            this.bPCSelectAll.Name = "bPCSelectAll";
            this.bPCSelectAll.Size = new System.Drawing.Size(79, 34);
            this.bPCSelectAll.TabIndex = 7;
            this.bPCSelectAll.Text = "Select All";
            this.bPCSelectAll.UseVisualStyleBackColor = true;
            this.bPCSelectAll.Click += new System.EventHandler(this.bPCSelectAll_Click);
            // 
            // bRun
            // 
            this.bRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRun.Location = new System.Drawing.Point(1055, 19);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(109, 34);
            this.bRun.TabIndex = 6;
            this.bRun.Text = "Go-go-go";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.bRun_Click);
            // 
            // chkList_PC
            // 
            this.chkList_PC.CheckOnClick = true;
            this.chkList_PC.FormattingEnabled = true;
            this.chkList_PC.HorizontalScrollbar = true;
            this.chkList_PC.Items.AddRange(new object[] {
            "m16",
            "riga",
            "t90",
            "pc1",
            "pc2",
            "pc3",
            "pc4",
            "pc5",
            "pc6",
            "pc7",
            "pc8",
            "pc9",
            "pc10",
            "pc11",
            "pc12",
            "pc13",
            "pc14",
            "pc15",
            "pc16",
            "pc17",
            "pc18",
            "pc19",
            "pc20"});
            this.chkList_PC.Location = new System.Drawing.Point(23, 71);
            this.chkList_PC.MultiColumn = true;
            this.chkList_PC.Name = "chkList_PC";
            this.chkList_PC.Size = new System.Drawing.Size(490, 232);
            this.chkList_PC.TabIndex = 5;
            // 
            // tabCommand
            // 
            this.tabCommand.Controls.Add(this.tabCopyFileFolder);
            this.tabCommand.Controls.Add(this.tabScript);
            this.tabCommand.Controls.Add(this.tabTaskCommand);
            this.tabCommand.Location = new System.Drawing.Point(-4, 487);
            this.tabCommand.Name = "tabCommand";
            this.tabCommand.SelectedIndex = 0;
            this.tabCommand.Size = new System.Drawing.Size(1172, 217);
            this.tabCommand.TabIndex = 4;
            // 
            // tabCopyFileFolder
            // 
            this.tabCopyFileFolder.Location = new System.Drawing.Point(4, 26);
            this.tabCopyFileFolder.Name = "tabCopyFileFolder";
            this.tabCopyFileFolder.Padding = new System.Windows.Forms.Padding(3);
            this.tabCopyFileFolder.Size = new System.Drawing.Size(1164, 187);
            this.tabCopyFileFolder.TabIndex = 0;
            this.tabCopyFileFolder.Text = "Copy File/Folder";
            this.tabCopyFileFolder.UseVisualStyleBackColor = true;
            // 
            // tabScript
            // 
            this.tabScript.Location = new System.Drawing.Point(4, 26);
            this.tabScript.Name = "tabScript";
            this.tabScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabScript.Size = new System.Drawing.Size(1164, 187);
            this.tabScript.TabIndex = 1;
            this.tabScript.Text = "Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // tabTaskCommand
            // 
            this.tabTaskCommand.Controls.Add(this.tCommand);
            this.tabTaskCommand.Controls.Add(this.lCommand);
            this.tabTaskCommand.Location = new System.Drawing.Point(4, 26);
            this.tabTaskCommand.Name = "tabTaskCommand";
            this.tabTaskCommand.Padding = new System.Windows.Forms.Padding(3);
            this.tabTaskCommand.Size = new System.Drawing.Size(1164, 187);
            this.tabTaskCommand.TabIndex = 2;
            this.tabTaskCommand.Text = "Command";
            this.tabTaskCommand.UseVisualStyleBackColor = true;
            // 
            // tCommand
            // 
            this.tCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tCommand.Location = new System.Drawing.Point(173, 61);
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
            this.lCommand.Location = new System.Drawing.Point(66, 87);
            this.lCommand.Name = "lCommand";
            this.lCommand.Size = new System.Drawing.Size(78, 18);
            this.lCommand.TabIndex = 3;
            this.lCommand.Text = "Command";
            this.lCommand.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.wResult);
            this.tabResult.Location = new System.Drawing.Point(4, 26);
            this.tabResult.Name = "tabResult";
            this.tabResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabResult.Size = new System.Drawing.Size(1176, 710);
            this.tabResult.TabIndex = 1;
            this.tabResult.Text = " Result ";
            this.tabResult.UseVisualStyleBackColor = true;
            // 
            // wResult
            // 
            this.wResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wResult.Location = new System.Drawing.Point(3, 3);
            this.wResult.MinimumSize = new System.Drawing.Size(20, 20);
            this.wResult.Name = "wResult";
            this.wResult.Size = new System.Drawing.Size(1170, 704);
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
            this.tabMainControl.Size = new System.Drawing.Size(1184, 740);
            this.tabMainControl.TabIndex = 7;
            // 
            // bAddPCFromDomain
            // 
            this.bAddPCFromDomain.Enabled = false;
            this.bAddPCFromDomain.Location = new System.Drawing.Point(545, 71);
            this.bAddPCFromDomain.Name = "bAddPCFromDomain";
            this.bAddPCFromDomain.Size = new System.Drawing.Size(209, 34);
            this.bAddPCFromDomain.TabIndex = 6;
            this.bAddPCFromDomain.Text = "Add From Domain";
            this.bAddPCFromDomain.UseVisualStyleBackColor = true;
            // 
            // bAddthisPC
            // 
            this.bAddthisPC.Location = new System.Drawing.Point(680, 109);
            this.bAddthisPC.Name = "bAddthisPC";
            this.bAddthisPC.Size = new System.Drawing.Size(74, 34);
            this.bAddthisPC.TabIndex = 6;
            this.bAddthisPC.Text = "Add this";
            this.bAddthisPC.UseVisualStyleBackColor = true;
            this.bAddthisPC.Click += new System.EventHandler(this.bAddthisPC_Click);
            // 
            // tThisPC
            // 
            this.tThisPC.Location = new System.Drawing.Point(545, 115);
            this.tThisPC.Name = "tThisPC";
            this.tThisPC.Size = new System.Drawing.Size(129, 24);
            this.tThisPC.TabIndex = 8;
            this.tThisPC.Text = "pc100";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(545, 149);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(209, 34);
            this.button3.TabIndex = 6;
            this.button3.Text = "Delete Selected";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.bRun_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 762);
            this.Controls.Add(this.tabMainControl);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remote Group Execution";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.tabTask.ResumeLayout(false);
            this.tabTask.PerformLayout();
            this.tabCommand.ResumeLayout(false);
            this.tabTaskCommand.ResumeLayout(false);
            this.tabTaskCommand.PerformLayout();
            this.tabResult.ResumeLayout(false);
            this.tabMainControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage tabTask;
        private System.Windows.Forms.TextBox tPCMaskSelect;
        private System.Windows.Forms.Button bPCSelectOnMask;
        private System.Windows.Forms.Button bPCUnSelectAll;
        private System.Windows.Forms.Button bPCSelectAll;
        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.CheckedListBox chkList_PC;
        private System.Windows.Forms.TabControl tabCommand;
        private System.Windows.Forms.TabPage tabCopyFileFolder;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.TabPage tabTaskCommand;
        private System.Windows.Forms.TextBox tCommand;
        private System.Windows.Forms.Label lCommand;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.WebBrowser wResult;
        private System.Windows.Forms.TabControl tabMainControl;
        private System.Windows.Forms.TextBox tThisPC;
        private System.Windows.Forms.Button bAddthisPC;
        private System.Windows.Forms.Button bAddPCFromDomain;
        private System.Windows.Forms.Button button3;
    }
}

