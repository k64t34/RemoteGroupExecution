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



namespace RGE
{
    
    public partial class Form1 : Form
    {
        const string _BR = "<BR>";
        const string _SP = "&nbsp;";
        const string _SP3 = "&nbsp;&nbsp;&nbsp;";
        int MainStatus = 0;// 0-stop, 1-run
        StreamWriter sw;
        string FileReport;
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
                Run_Copy();



                

                Stop_Run();
            }
            finally{}            

        }
        private void Stop_Run()
        {
            MainStatus = 0;
            ToolbGo.BackColor = Color.Lime;
            ToolbGo.Text = "     Go     ";
            ToolbGo.ForeColor = Color.Black;
            WriteLog(D_T() + t_color("white", " Finish") + _BR+"\n");
            WriteLog("\n</body></html>");
            sw = new StreamWriter(FileReport);
            sw.Write(wResult.DocumentText);
            sw.Close();
            wResult.Navigate("file://" + FileReport);
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


        

        void Run_Copy()
        {
            StringBuilder Output = new StringBuilder();
            Output.Append(t_color("yellow", "Host:") + _BR + "\n");
            Output.Append(t_color("yellow", "Copy from:") + tSourceCopy.Text);                        
            if (!Directory.Exists(tSourceCopy.Text))
                { 
                Output.Append(t_color("red", " Folder doesn't exist"));
                Output.Append(_BR + "\n");
                WriteLog(Output);
                return;
                }
            Output.Append(_BR + "\n");
            WriteLog(Output);
            WriteLog(t_color("yellow", "Copy to  :") + tTargetCopy.Text + _BR + "\n");
            WriteLog(t_color("yellow", "Override:") + chkCopyOverride.Checked + _BR + "\n");
            if (chkCopyOverride.Checked)
                WriteLog(t_color("yellow", "Only newer:") + chkCopyOnlyNewer.Checked + _BR + "\n");

            int i = 0;
            foreach (int indexChecked in chkList_PC.CheckedIndices)
            {
                WriteLog(String.Format("{0}.{1}<br>\n", ++i, chkList_PC.Items[indexChecked].ToString()));
            }
            WriteLog("<br>\n");
            i = 0;
            foreach (int indexChecked in chkList_PC.CheckedIndices) Do_Copy(indexChecked);
            WriteLog("<br>\n");
            /*While ThreadCount> 0
            Console.Writeline("Ожидание завершения " + CStr(ThreadCount) + " процессов поиска МАК адресов")
            Threading.Thread.Sleep(1000)
            End While
            Threading.Thread.Sleep(1000)*/

            /*
             https://habr.com/ru/post/165729/
             ThreadPool Class https://docs.microsoft.com/en-us/dotnet/api/system.threading.threadpool?redirectedfrom=MSDN&view=net-5.0
             */

        }
        void Do_Copy(int Index)
        {
            StringBuilder Output = new StringBuilder();
            Output.Append(chkList_PC.Items[Index].ToString());
            //Работа с потоками в C# http://rsdn.org/article/dotnet/CSThreading1.xml

            /*Public Dim  MAXThreadCount As Integer = 100
            While ThreadCount> MAXThreadCount
                Threading.Thread.Sleep(1000)
            End While
            Dim myTest As New ThreadsGetMACfromHOST(client.Name)
            Dim bThreadStart As New ThreadStart(AddressOf myTest.GetMACfromHOST)
            Dim bThread As New Thread(bThreadStart)
            bThread.Start*/
            Output.Append("<br>\n");
            WriteLog(Output);

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
        }
    }

   
}
