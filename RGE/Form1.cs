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
    
    public  partial class Form1 : Form
    {
        CancellationTokenSource cts;
        static RGE.Form1 THIS;
        static string __Error;
        const string _BR = "<BR>";
        const string _LF = "\n";
        const string _CR = "\r";
        const string _CRLF = "\r\n";
        const string _TAB = "\t";
        const string _SP = "&nbsp;";
        const string _SP3 = "&nbsp;&nbsp;&nbsp;";
        static int MainStatus = 0;// 0-stop, 1-run
        StreamWriter sw;
        string FileReport;
        int __PROCESSOR_COUNT=2;
        const int __CMDLineMaximumLength = 2047;//8191
#if DEBUG
        const int __THREAD_MULTI = 4;
#else
        const int __THREAD_MULTI = 8;
#endif

        static int RunningThreadCount = 0;
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
            String DateReport = DateTime.Now.ToString("ddMMyyyy-HHmmss");            
            string userNameWin, compName, compIP, myHost;
            StringBuilder Output = new StringBuilder();           
            try
            {
                FileReport = Environment.CurrentDirectory + "\\" + DateReport + ".html";
                sw = new StreamWriter(FileReport);
                wResult.DocumentText = "";
                // имя хоста
                myHost = System.Net.Dns.GetHostName();
                // IP по имени хоста, выдает список, можно обойти в цикле весь, здесь берется первый адрес
                compIP = System.Net.Dns.GetHostEntry(myHost).AddressList[1].ToString();
                userNameWin = System.Environment.UserName;
                compName = System.Environment.MachineName;
                Output.Append("<!--  "+ myHost + " " + compIP + " " + userNameWin + " " + compName+ "-->" + _LF);
                Output.Append("<html>\n" +
                    "\t<head>\n" +
                    "\t\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\n" +
                    "\t\t<meta http-equiv=\"x-ua-compatible\" content=\"IE=edge\">\n"+
                    /*"\t\t<link rel=\"stylesheet\" type=\"text/css\" href=\"default.css\">\n" +*/
                    /*"\t\t<title> Отчет "+ DateReport + "</title>\n"+                    */
                    "\t</head>\n"+
                    "<STYLE>\n"+File.ReadAllText("default.css")+ "<\n/STYLE>\n"+
                    "<body>\n");                
                Output.Append(D_T() + HTMLTagSpan(" Remote group execution start","EVENT") + _BR+"\n");
                
                WriteLog(Output);
                
                //sw.Write(Output);
                //sw.Close();
                //wResult.Navigate(FileReport);
                //sw = new StreamWriter(FileReport, true);

                WriteLog(t_color("white", "Report file:")+" "+FileReport + _BR + "\n");

                //wResult.Document.Body.ScrollIntoView(true); 
                
                cts = new CancellationTokenSource();
                //var Token = cts.Token;
                //Run_Copy();
            }
            finally{}            

        }
        private void End_Run()
        {
            
            cts.Dispose();

            WriteLog("<p /ID=\"finish\"/>" + D_T() + t_color("white", " Finish") + "</p>" + _BR + "\n\n</body></html>");
            
            //if (wResult.InvokeRequired)
            //{
                
                Invoke(new Action(() =>
                {
                    //sw.Write(wResult.DocumentText);
                    //wResult.Navigate(FileReport);
                    //Thread.Sleep(100);
                    //wResult.Document.GetElementById("finish").ScrollIntoView(true);
                }));
            //}
            //else
            //{
            //    //wResult.Document.Body.ScrollIntoView(false);
            //    //wResult.Navigate("file://" + FileReport);            
            //    sw.Write(wResult.DocumentText);
            //    wResult.Document.GetElementById("finish").ScrollIntoView(true);
            //}
            sw.Close();

            MainStatus = 0;
            ToolbGo.BackColor = Color.Lime;
            ToolbGo.Text = "     Go     ";
            ToolbGo.ForeColor = Color.Black;
            ToolbGo.Enabled = true;
        }
        private void Stop_Run()
        {
            MainStatus = 0;
            if (RunningThreadCount!=0)
            {
#if DEBUG
                Debug.WriteLine("CANCEL");
#endif                                
                WriteLog(D_T() + t_color("red", " Cancel") + _BR + "\n");                
                ToolbGo.Enabled = false;
                ToolbGo.BackColor = Color.Yellow;
                ToolbGo.Text = " Waiting.. ";
                ToolbGo.ForeColor = Color.Black;
                cts.Cancel();
            }
        }
        static void WriteLog(string message)
        {
            if (THIS.wResult.InvokeRequired)
            {
                THIS.wResult.BeginInvoke(new Action<string>((s) => THIS.wResult.Document.Write(s)), message);
                //THIS.wResult.DocumentText += message;
            }
            else
            {
                //THIS.wResult.Document.Write(message);
                THIS.wResult.DocumentText += message;
            }
        }
        static void WriteLog(StringBuilder message)
        {
            if (THIS.wResult.InvokeRequired)
            {
                THIS.wResult.BeginInvoke(new Action<string>((s) => THIS.wResult.Document.Write(s)), message.ToString());
                //THIS.wResult.DocumentText += message;
            }
            else
            {
                THIS.wResult.DocumentText += message;
            }
        }
        






        static string D_T() { return DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"); }
        static string t_color(string color, string format, params string[] text) { return "<font color=\""+ color + "\">"+ String.Format(format, text) + "</font>"; }
        static string t_color(string color, string format) { return t_color(color, format, ""); }

        Func<String, String, String> HTMLTagSpan = (String InnerText, String Class) =>  "<span class=\"" + Class + "\">" + InnerText + "</span>";
        
        private void chkCopyOverride_Validated(object sender, EventArgs e)
        {
            chkCopyOnlyNewer.Enabled = chkCopyOverride.Checked;            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            THIS = this;
            bPCSelectAll_Click( sender,  e);

#if !DEBUG
        __PROCESSOR_COUNT = Environment.ProcessorCount;
#endif
            ThreadCount = __PROCESSOR_COUNT * __THREAD_MULTI;
            //for (int i = 0; i != ThreadCount; i++) Threads.Add(new Thread());
#if DEBUG
            CheckForIllegalCrossThreadCalls = false;
#endif

#if DEBUG
            Debug.WriteLine("Processor count: "+ __PROCESSOR_COUNT.ToString());
#endif
            
        }
        static Boolean Ping(String Host)
        {
            bool Ping = false;
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply reply = ping.Send(Host);
                if (reply.Status.ToString() == "Success") Ping = true;
            }
            catch (Exception e) { /*__Error = e.ToString();*/ }
            return (Ping);
        }
    }

   
}





/*
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta http-equiv="x-ua-compatible" content="IE=edge">
		<title>Отчет </title>
		<link rel="stylesheet" type="text/css" href="default.css">
	</head>
	<body>
01/02/2021 <span class="EVENT"> Start</span>  <br>
<input type="checkbox" id="PC1" class="PC"/>
    <label for="PC1" >PC1 <span class="OK">OK</span></label>
    <div>
        Ping <span class="OK">OK</span><br>
		WMI <span class="OK">OK</span> <br>
		Copy <span class="OK">OK</span><br>		
		</div><br>	
https://csharp.hotexamples.com/ru/examples/-/TagBuilder/-/php-tagbuilder-class-examples.html		

var items=Source "<span class="OK">OK</span><br>"

var div = new TagBuilder("div");
div.InnerHtml =items;
div.InnerHtml.append(items);
div.ToString(TagRenderMode.Normal);
		
<input type="checkbox" id="PC2" class="PC"/>
    <label for="PC2" >PC2 <span class="FAULT">FAULT</span> </label>
    <div>
        Ping <span class="OK">OK</span><br>
		WMI <span class="OK">OK</span> <br>
		<input type="checkbox" id="PC2.Copy" class="PC"/>
		<label for="PC2.Copy" >Copy <span class="FAULT">FAULT</span> </label>
		<div>
		    Source <span class="OK">OK</span><br>
			Target <span class="OK">OK</span><br>
			<input type="checkbox" id="PC2.Copy.Access" class="PC"/>
			<label for="PC2.Copy.Access" >Access <span class="FAULT">FAULT</span></label>
			<div>
				Нет прав доступа <br>				
			</div>			
		</div>		
	</div><br>	
<input type="checkbox" id="PC1" class="PC"/>
    <label for="PC1" >PC1</label>
    <div>
        Ping OK<br>
		WMI OK <br>
		Copy <br>		
		</div><br>	
<input type="checkbox" id="PC1" class="PC"/>
    <label for="PC1" >PC1</label>
    <div>
        Ping OK<br>
		WMI OK <br>
		Copy <br>		
		</div><br>			
01/02/2021 <span class="EVENT"> CANCEL</span>  <br>		
<input type="checkbox" id="hd-1" class="hide"/>
    <label for="hd-1" >Нажмите здесь, чтобы увидеть скрытый текст №1</label>
    <div>
        HTML — стандартный язык разметки документов во Всемирной паутине. Большинство веб-страниц содержат описание разметки на языке HTML (или XHTML). Язык HTML интерпретируется браузерами и отображается в виде документа в удобной для человека форме..
    </div><br>
	

01/02/2021 <span id="Finish" class="EVENT"> Finish</span>  <br>
	</body>
</html>	
<!--- https://dbmast.ru/raskryvayushhiesya-bloki-s-skrytym-soderzhaniem-s-pomoshhyu-css3----> 
 
 */