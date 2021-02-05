using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace RGE
{
    
    public partial class Form1 : Form
    {
        string __Error;
        const string _BR = "<BR>";
        const string _SP = "&nbsp;";
        const string _SP3 = "&nbsp;&nbsp;&nbsp;";
        int MainStatus = 0;// 0-stop, 1-run
        StreamWriter sw;
        string FileReport;
        int __PROCESSOR_COUNT=2;
        const int __CMDLineMaximumLength = 2047;//8191
#if DEBUG
        const int __THREAD_MULTI = 4;
#else
        const int __THREAD_MULTI = 8;
#endif

        int ThreadCount = 8;
        int FreeThreadCount = 8;
        List<Thread> Threads = new List<Thread>();
        public Form1()
        {
            InitializeComponent();
        }

        private void bRun_Click(object sender, EventArgs e)
        {

            this.tabMainControl.SelectTab(this.tabResult);
            

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //tCommand.Width = bRun.Left - tCommand.Margin.All - tCommand.Left;
        }

        private void bPCSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i != chkList_PC.Items.Count; i++)
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
                if (chkList_PC.FindString(tThisPC.Text) == ListBox.NoMatches)
                    chkList_PC.Items.Add(tThisPC.Text, CheckState.Checked);

            }
        }

        private void tabCommandControl_DrawItem(object sender, DrawItemEventArgs e)
        {         

        }

        private void tabComandScript_Click(object sender, EventArgs e)
        {
            tabCommandControl.SelectTab(tabCommandCopyFileFolder);
        }

        private void lTargetCopy_Click(object sender, EventArgs e)
        {

        }

        private void bTargetCopy_Click(object sender, EventArgs e)
        {
            /*if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                tTargetCopy.Text = folderBrowserDialog1.SelectedPath;*/
            saveFileDialog1.Title = "Target path";
            saveFileDialog1.FileName = "Save Here";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                tTargetCopy.Text = Path.GetDirectoryName(saveFileDialog1.FileName);
        }

        private void bSourceCopy_Click(object sender, EventArgs e)
        {
            /*if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                tTargetCopy.Text = folderBrowserDialog1.SelectedPath;*/
            saveFileDialog1.Title = "Source path";
            saveFileDialog1.FileName = "From Here";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                tSourceCopy.Text = Path.GetDirectoryName(saveFileDialog1.FileName);            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ToolbGo_Click(object sender, EventArgs e)
        {
            if (MainStatus == 0)
            {                       
                Begin_Run();
            }
            else if (MainStatus == 1)
            {                
                Stop_Run();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tabMainControl.SelectTab(this.tabResult);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tabMainControl.SelectTab(this.tabTask);
        }
        private void Begin_Run()
        {
            MainStatus = 1;
            ToolbGo.BackColor = Color.Red;
            ToolbGo.Text = "    Stop    ";
            ToolbGo.ForeColor = Color.White;
            tabMainControl.SelectTab(this.tabResult);
            FileReport = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("ddMMyyyy-HHmmss") + ".html";
            string userNameWin, compName, compIP, myHost;
            try
            {
                // имя хоста
                myHost = System.Net.Dns.GetHostName();
                // IP по имени хоста, выдает список, можно обойти в цикле весь, здесь берется первый адрес
                compIP = System.Net.Dns.GetHostEntry(myHost).AddressList[1].ToString();
                userNameWin = System.Environment.UserName;
                compName = System.Environment.MachineName;
                WriteLog(String.Format("<!--{0} {1} {2} {3} -->\n", myHost, compIP, userNameWin, compName));
                WriteLog("<html>\n<meta charset=\"utf-8\">\n<style>\nbody{background-color: black;color:grey;font-family: monospace;font-size:16;padding:0}\n</style>\n<body text=\"grey\" bgcolor=\"black\">\n");
                WriteLog(D_T() + t_color("white", " Remote group execution start") + _BR+"\n");
                WriteLog(t_color("white", "Report file:")+" "+FileReport + _BR + "\n");
                wResult.Document.Body.ScrollIntoView(true);
                Run_Copy();
            }
            finally{}            

        }
        private void Stop_Run()
        {
            MainStatus = 0;
            ToolbGo.BackColor = Color.Lime;
            ToolbGo.Text = "     Go     ";
            ToolbGo.ForeColor = Color.Black;
            WriteLog("<p /ID=\"finish\"/>" + D_T() + t_color("white", " Finish") + "</p>" + _BR + "\n\n</body></html>");
            sw = new StreamWriter(FileReport);            
            if (wResult.InvokeRequired)
            {
                //TODO: Stop all threads                
                Invoke(new Action(() =>
                {
                    sw.Write(wResult.DocumentText);
                    wResult.Document.GetElementById("finish").ScrollIntoView(true);                    
                }));
            }       
            else
            {    
                //wResult.Document.Body.ScrollIntoView(false);
                //wResult.Navigate("file://" + FileReport);            
                sw.Write(wResult.DocumentText);
                wResult.Document.GetElementById("finish").ScrollIntoView(true);
            }            
            sw.Close();
        }
        void WriteLog(string message)
        {
            if (wResult.InvokeRequired)
            {
                wResult.BeginInvoke(new Action<string>((s) => wResult.Document.Write(s)), message);
            }
            else
            {
                wResult.Document.Write(message);
            }
        }
        void WriteLog(StringBuilder message)
        {
            if (wResult.InvokeRequired)
            {
                wResult.BeginInvoke(new Action<string>((s) => wResult.Document.Write(s)), message.ToString());
            }
            else
            {
                wResult.Document.Write(message.ToString());
            }
        }


        

       
      
        string D_T() { return DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"); }
        string t_color(string color, string format, params string[] text) { return "<font color=\""+ color + "\">"+ String.Format(format, text) + "</font>"; }
        string t_color(string color, string format) { return t_color(color, format, ""); }

        private void chkCopyOverride_Validated(object sender, EventArgs e)
        {
            chkCopyOnlyNewer.Enabled = chkCopyOverride.Checked;            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bPCSelectAll_Click( sender,  e);

#if !DEBUG
        __PROCESSOR_COUNT = Environment.ProcessorCount;
#endif
            ThreadCount = __PROCESSOR_COUNT * __THREAD_MULTI;
            for (int i = 0; i != ThreadCount; i++) Threads.Add(new Thread());
#if DEBUG
            CheckForIllegalCrossThreadCalls = false;
#endif

#if DEBUG
            Debug.WriteLine("Processor count: "+ __PROCESSOR_COUNT.ToString());
#endif
            
        }
        Boolean Ping(String Host)
        {
            bool Ping = false;
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply reply = ping.Send(Host);
                if (reply.Status.ToString() == "Success") Ping = true;
            }
            catch (Exception e) { __Error = e.ToString(); }
            return (Ping);
        }
    }

   
}
