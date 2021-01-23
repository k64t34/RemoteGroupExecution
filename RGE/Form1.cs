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

        private void bRun_Click(object sender, EventArgs e)
        {

            this.tabMainControl.SelectTab(this.tabResult);            
            string u = "file://" + Environment.CurrentDirectory + "\\result1.html";
            wResult.Navigate(u);
            
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            tCommand.Width = bRun.Left - tCommand.Margin.All - tCommand.Left;
        }

        private void bPCSelectAll_Click(object sender, EventArgs e)
        {
            for (int i=0; i!=chkList_PC.Items.Count;i++)
            {
                chkList_PC.SetItemChecked(i, true);
            }
        }

        private void bPCUnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (int indexChecked in chkList_PC.CheckedIndices)
            {
                chkList_PC.SetItemChecked(indexChecked, false);
            }
        }

        private void bAddthisPC_Click(object sender, EventArgs e)
        {
            for (int i = 0; i != chkList_PC.Items.Count; i++)
            {
                if (chkList_PC.FindString(tThisPC.Text)== ListBox.NoMatches)
                    chkList_PC.Items.Add(tThisPC.Text, CheckState.Checked);
                
            }
        }
    }
}
