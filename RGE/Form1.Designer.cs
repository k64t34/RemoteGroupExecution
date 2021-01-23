
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTask = new System.Windows.Forms.TabPage();
            this.tCommand = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bRun = new System.Windows.Forms.Button();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.wResult = new System.Windows.Forms.WebBrowser();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabCopyFileFolder = new System.Windows.Forms.TabPage();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabTask.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 739);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTask);
            this.tabControl1.Controls.Add(this.tabResult);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1184, 739);
            this.tabControl1.TabIndex = 7;
            // 
            // tabTask
            // 
            this.tabTask.Controls.Add(this.tCommand);
            this.tabTask.Controls.Add(this.label1);
            this.tabTask.Controls.Add(this.bRun);
            this.tabTask.Location = new System.Drawing.Point(4, 31);
            this.tabTask.Name = "tabTask";
            this.tabTask.Padding = new System.Windows.Forms.Padding(3);
            this.tabTask.Size = new System.Drawing.Size(1176, 704);
            this.tabTask.TabIndex = 0;
            this.tabTask.Text = " Task ";
            this.tabTask.UseVisualStyleBackColor = true;
            // 
            // tCommand
            // 
            this.tCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tCommand.Location = new System.Drawing.Point(115, 617);
            this.tCommand.Multiline = true;
            this.tCommand.Name = "tCommand";
            this.tCommand.Size = new System.Drawing.Size(926, 81);
            this.tCommand.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 643);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // bRun
            // 
            this.bRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRun.Location = new System.Drawing.Point(1049, 633);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(109, 34);
            this.bRun.TabIndex = 0;
            this.bRun.Text = "Go-go-go";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.button1_Click);
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
            this.wResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wResult.Location = new System.Drawing.Point(3, 3);
            this.wResult.MinimumSize = new System.Drawing.Size(20, 20);
            this.wResult.Name = "wResult";
            this.wResult.Size = new System.Drawing.Size(1170, 698);
            this.wResult.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Controls.Add(this.checkedListBox1);
            this.tabPage3.Controls.Add(this.tabControl2);
            this.tabPage3.Location = new System.Drawing.Point(4, 31);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1176, 704);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabCopyFileFolder);
            this.tabControl2.Controls.Add(this.tabScript);
            this.tabControl2.Enabled = false;
            this.tabControl2.Location = new System.Drawing.Point(-4, 487);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1172, 217);
            this.tabControl2.TabIndex = 4;
            // 
            // tabCopyFileFolder
            // 
            this.tabCopyFileFolder.Location = new System.Drawing.Point(4, 31);
            this.tabCopyFileFolder.Name = "tabCopyFileFolder";
            this.tabCopyFileFolder.Padding = new System.Windows.Forms.Padding(3);
            this.tabCopyFileFolder.Size = new System.Drawing.Size(1164, 182);
            this.tabCopyFileFolder.TabIndex = 0;
            this.tabCopyFileFolder.Text = "Copy File/Folder";
            this.tabCopyFileFolder.UseVisualStyleBackColor = true;
            // 
            // tabScript
            // 
            this.tabScript.Location = new System.Drawing.Point(4, 31);
            this.tabScript.Name = "tabScript";
            this.tabScript.Padding = new System.Windows.Forms.Padding(3);
            this.tabScript.Size = new System.Drawing.Size(1164, 182);
            this.tabScript.TabIndex = 1;
            this.tabScript.Text = "Script";
            this.tabScript.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.HorizontalScrollbar = true;
            this.checkedListBox1.Items.AddRange(new object[] {
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
            this.checkedListBox1.Location = new System.Drawing.Point(23, 33);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(490, 418);
            this.checkedListBox1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1055, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "Go-go-go";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remote Group Execution";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.tabControl1.ResumeLayout(false);
            this.tabTask.ResumeLayout(false);
            this.tabTask.PerformLayout();
            this.tabResult.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTask;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.TextBox tCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser wResult;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabCopyFileFolder;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
    }
}

