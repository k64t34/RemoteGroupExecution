using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.tabControl1.SelectTab(this.tabResult);            
            string u = "file://" + Environment.CurrentDirectory + "\\result1.html";
            wResult.Navigate(u);
            
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            tCommand.Width = bRun.Left - tCommand.Margin.All - tCommand.Left;
        }
    }
}
