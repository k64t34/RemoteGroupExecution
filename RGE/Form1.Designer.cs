
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
            this.tabResult = new System.Windows.Forms.TabPage();
            this.bRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tCommand = new System.Windows.Forms.TextBox();
            this.wResult = new System.Windows.Forms.WebBrowser();
            this.tabControl1.SuspendLayout();
            this.tabTask.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 731);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1182, 22);
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
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1182, 731);
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
            this.tabTask.Size = new System.Drawing.Size(1174, 696);
            this.tabTask.TabIndex = 0;
            this.tabTask.Text = " Task ";
            this.tabTask.UseVisualStyleBackColor = true;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.wResult);
            this.tabResult.Location = new System.Drawing.Point(4, 31);
            this.tabResult.Name = "tabResult";
            this.tabResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabResult.Size = new System.Drawing.Size(1174, 696);
            this.tabResult.TabIndex = 1;
            this.tabResult.Text = " Result ";
            this.tabResult.UseVisualStyleBackColor = true;
            // 
            // bRun
            // 
            this.bRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bRun.Location = new System.Drawing.Point(1047, 625);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(109, 34);
            this.bRun.TabIndex = 0;
            this.bRun.Text = "Go-go-go";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 635);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tCommand
            // 
            this.tCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tCommand.Location = new System.Drawing.Point(115, 609);
            this.tCommand.Multiline = true;
            this.tCommand.Name = "tCommand";
            this.tCommand.Size = new System.Drawing.Size(926, 81);
            this.tCommand.TabIndex = 2;
            // 
            // wResult
            // 
            this.wResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wResult.Location = new System.Drawing.Point(3, 3);
            this.wResult.MinimumSize = new System.Drawing.Size(20, 20);
            this.wResult.Name = "wResult";
            this.wResult.Size = new System.Drawing.Size(1168, 690);
            this.wResult.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1182, 753);
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
    }
}

