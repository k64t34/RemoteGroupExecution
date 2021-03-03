﻿using System.Configuration;
using System.Collections;
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
using System.DirectoryServices;

namespace RGE
{
    public delegate System.Windows.Forms.HtmlElement    delegateCreateElement(string elementTag);
    public delegate void                                delegateUpdateElement(HtmlElement htmlElement);
    public delegate System.Windows.Forms.HtmlElement    delegateGetElement(string elementId);
    public  partial class Form1 : Form
    {        
        CancellationTokenSource cts;
        static RGE.Form1 THIS;
        static string __Error;
        const string _BR = "<BR>";
        const string _BRLF = "<BR>\n";
        const string _LF = "\n";
        const string _CR = "\r";
        const string _CRLF = "\r\n";
        const string _TAB = "\t";
        const string _SP = "&nbsp;";
        const string _SP3 = "&nbsp;&nbsp;&nbsp;";
        const string CSS_BaseHighLight = "BaseHighLight";
        const string CSS_BaseAlert = "BaseAlert";
        const string CSS_FAULT = "FAULT";
        const string CSS_OK = "OK";
        const string CSS_CANCEL = "CANCEL";
        const string CSS_WARM = "WARM";
        static int MainStatus = 0;// 0-stop, 1-run
        StreamWriter sw;
        string FileReport;
        int __PROCESSOR_COUNT=1;
        const int __CMDLineMaximumLength = 2047;//8191
#if DEBUG
        const int __THREAD_MULTI = 4;
#else
        const int __THREAD_MULTI = 8;
#endif

        static int RunningThreadCount = 0;
        int ThreadCount = 8;
        static String ScriptFullPathName = Application.ExecutablePath;
        static String ScriptFolder = Path.GetDirectoryName(ScriptFullPathName); 
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
                Cancel_Run();
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
            StringBuilder Output = new StringBuilder();
            try
            {
                if (!tSourceCopy.Text.EndsWith("\\")) tSourceCopy.Text += "\\";

                myHost = System.Net.Dns.GetHostName();// имя хоста                
                compIP = System.Net.Dns.GetHostEntry(myHost).AddressList[1].ToString();// IP по имени хоста, выдает список, можно обойти в цикле весь, здесь берется первый адрес
                userNameWin = System.Environment.UserName;
                compName = System.Environment.MachineName;
                //WriteLog(String.Format("<!--{0} {1} {2} {3} -->\n", myHost, compIP, userNameWin, compName));
                Output.Append("<!--  " + myHost + " " + compIP + " " + userNameWin + " " + compName + "-->" + _LF +
                    "<html>\n\t<head>\n" +
                    "\t\t<meta charset=\"utf-8\">\n" +
                    "\t\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\n" +
                    "\t\t<meta http-equiv=\"x-ua-compatible\" content=\"IE=edge\">\n" +
                    //"\t\t<link rel=\"stylesheet\" type=\"text/css\" href=\"default.css\">\n" +
                    "\t</head>\n" +
                    "<style>\n" + File.ReadAllText("default.css") + "\n</style>\n" +
                    "<body>\n" +
                    D_T() + HTMLTagSpan(" Start", "EVENT") + _BRLF +
                    HTMLTagSpan("Report file:", CSS_BaseHighLight) + FileReport + _BRLF+
                    HTMLTagSpan("Source path:", CSS_BaseHighLight) + tSourceCopy.Text + _BRLF +
                    "</body></html>"
                    );
                    WriteLog(Output);                
                    wResult.Document.Body.ScrollIntoView(false);


                /*HtmlElement divFile = wResult.Document.CreateElement("div");
                divFile.SetAttribute("id", "l1");
                divFile.InnerHtml = "Level 1";
                HtmlElement divFile2 = wResult.Document.CreateElement("div");
                divFile2.SetAttribute("id", "l2");
                divFile2.InnerHtml = "Level 2";
                divFile.AppendChild(divFile2);
                wResult.Document.Body.AppendChild(divFile);
                divFile = wResult.Document.GetElementById("l1");
                divFile.InnerHtml += " add";
                divFile = wResult.Document.GetElementById("l2");
                divFile.InnerHtml += " add2";*/
                cts = new CancellationTokenSource();                
                CopyFiles();                    
            }
            catch (Exception e)
            {
#if DEBUG
                __Error = e.ToString();
#else
                __Error = e.Message;
#endif
            }
            finally { }
                #region 
                /* Didn't work
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
                        //"\t\t<link rel=\"stylesheet\" type=\"text/css\" href=\"default.css\">\n" +
                        //"\t\t<title> Отчет "+ DateReport + "</title>\n"+                    
                        "\t</head>\n"+
                        "<style>\n" + File.ReadAllText("default.css")+ "\n</style>\n" +
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
                finally{}  */
                /* It's work good
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
                 */
                #endregion

            
        }
        public  HtmlElement CreateHtmlElement(String elementTag)
        {
            HtmlElement Result = wResult.Document.CreateElement(elementTag);
            return  (HtmlElement)Result;
        }
        public HtmlElement GetHtmlElement(String elementId)
        {
            HtmlElement Result = wResult.Document.GetElementById(elementId);
            return (HtmlElement)Result;
        }
        public void UpdateHtmlElement(HtmlElement div)
        {
            wResult.Document.Body.AppendChild(div);
            //wResult.Refresh();
        }
       
        async void CopyFiles()
        {
            await Task.Run(() =>
            {   
                delegateCreateElement c = new delegateCreateElement(CreateHtmlElement);
                delegateUpdateElement u = new delegateUpdateElement(UpdateHtmlElement);
                delegateGetElement    g = new delegateGetElement(GetHtmlElement);
        /*
        HtmlElement divFile;
        divFile = (HtmlElement)THIS.Invoke(c, "div");                
        divFile.SetAttribute("id", "div.Files");
        divFile.InnerHtml = "FILES";
        BeginInvoke(u, divFile);               */

        CopyFilesQueue CopyQ = new CopyFilesQueue(tSourceCopy.Text, tTargetCopy.Text/*, wResult/*.Document*/, chkList_PC,cts.Token,c,u,g);
                CopyQ.Copy();                
                cts.Cancel();                
                End_Run();
            });
            }
                
        public void End_Run()
        {            
            cts.Dispose();
            //WriteLog(D_T() + HTMLTagSpanID(" Finish", "EVENT","Finish") + _BRLF + "\n\n</body></html>");                 //"<p /ID=\"finish\"/>" + D_T() + t_color("white", " Finish") + "</p>" + _BR + "\n\n</body></html>");
            sw = new StreamWriter(FileReport);
            
            if (wResult.InvokeRequired)
            {                
                Invoke(new Action(() =>
                {
                    wResult.Document.Body.InnerHtml+=
                    _BR + D_T() + HTMLTagSpanID(" Finish", "EVENT", "Finish");
                    sw.Write(wResult.Document.Body.Parent.OuterHtml);
                    sw.Close();
                    wResult.Navigate("file://" + FileReport);
                    //Thread.Sleep(1000);
                    //wResult.Document.Body.ScrollIntoView(false);
                    //THIS.wResult.Document.GetElementById("finish").ScrollIntoView(false);
                    //Thread.Sleep(1000);
                    //THIS.wResult.Document.GetElementById("finish").ScrollIntoView(false);
                    //Thread.Sleep(1000);
                    //THIS.wResult.Document.GetElementById("finish").ScrollIntoView(true);

                }));
            }
            else
            {
                wResult.Document.Body.InnerHtml +=
                    _BR+D_T() + HTMLTagSpanID(" Finish", "EVENT", "Finish") ;
                sw.Write(wResult.Document.Body.Parent.OuterHtml);
                sw.Close();
                wResult.Navigate("file://" + FileReport);
                //Thread.Sleep(1000);
                //wResult.Document.Body.ScrollIntoView(false);
                //wResult.Document.GetElementById("finish").ScrollIntoView(false);
            }
            //sw.Close();
            MainStatus = 0;
            ToolbGo.BackColor = Color.Lime;
            ToolbGo.Text = "     Go     ";
            ToolbGo.ForeColor = Color.Black;
            ToolbGo.Enabled = true;
        }
        private void Cancel_Run()
        {
            //MainStatus = 0;            
            //if (RunningThreadCount!=0)
            {
#if DEBUG
                Debug.WriteLine("CANCEL");
#endif
                wResult.Document.Body.InnerHtml +=                    _BRLF + D_T() + HTMLTagSpanID(" Cancel", "EVENT", "Cancel") + _BRLF;                
                ToolbGo.Enabled = false;
                ToolbGo.BackColor = Color.Yellow;
                ToolbGo.Text = " Waiting.. ";
                ToolbGo.ForeColor = Color.Black;                
            }
            cts.Cancel();
        }
        /*static void WriteLog(string message)
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
        }*/

        static void WriteLog(string message)
        {
            if (THIS.wResult.InvokeRequired)
            {
                THIS.wResult.BeginInvoke(new Action<string>((s) => {
                    THIS.wResult.Document.Write(s);
                    THIS.wResult.Document.Body.ScrollIntoView(false);
                }), message);
                
            }
            else
            {
                THIS.wResult.Document.Write(message);
                THIS.wResult.Document.Body.ScrollIntoView(false);
            }
        }
        static void WriteLog(StringBuilder message)
        {
            if (THIS.wResult.InvokeRequired)
            {
                THIS.wResult.BeginInvoke(new Action<string>((s) => {
                    THIS.wResult.Document.Write(s);
                    THIS.wResult.Document.Body.ScrollIntoView(false);
                }), message.ToString());
                
            }
            else
            {
                THIS.wResult.Document.Write(message.ToString());
                THIS.wResult.Document.Body.ScrollIntoView(false);
            }
        }

        static string D_T() { return DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"); }
        static string t_color(string color, string format, params string[] text) { return "<font color=\""+ color + "\">"+ String.Format(format, text) + "</font>"; }
        static string t_color(string color, string format) { return t_color(color, format, ""); }

        static Func<String, String, String> HTMLTagSpan = (String InnerText, String Class) =>  "<span class=\"" + Class + "\">" + InnerText + "</span>";
        Func<String, String, String,String> HTMLTagSpanID = (String InnerText, String Class, String ID) => "<span id=\""+ID+"\" class=\"" + Class + "\">" + InnerText + "</span>";
        static Func<String> HTMLSpanOK   =                               () => "<span class=\"" + CSS_OK    + "\"> OK </span>";
        static Func<String> HTMLSpanFAULT =                               () => "<span class=\"" + CSS_FAULT + "\"> FAULT </span>";
        static Func<String> HTMLSpanCANCEL = () => "<span class=\"" + CSS_CANCEL + "\"> CANCEL </span>";

        private void chkCopyOverride_Validated(object sender, EventArgs e)
        {
            chkCopyOnlyNewer.Enabled = chkCopyOverride.Checked;            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            THIS = this;
            bPCSelectAll_Click( sender,  e);
            ThreadCount = __PROCESSOR_COUNT * __THREAD_MULTI;            
#if DEBUG
            CheckForIllegalCrossThreadCalls = false;
#endif
            #region Read setting
            tSourceCopy.Text = Config.ReadSetting("SourceFolder");
            tTargetCopy.Text = Config.ReadSetting("TargetFolder");
            Config.ReadSettingsHosts(chkList_PC);
            #endregion



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
            catch /*(Exception e)*/ { /*__Error = e.ToString();*/ }
            return (Ping);
        }
      
        private void bDeletePCSelected_Click(object sender, EventArgs e)
        {
            //https://docs.microsoft.com/ru-ru/dotnet/api/system.console.beep?view=net-5.0

            /*Note[] Major ={
                new Note(Tone.C, Duration.SIXTEENTH),
                new Note(Tone.E, Duration.SIXTEENTH),
                new Note(Tone.G, Duration.SIXTEENTH),
                new Note(Tone.E, Duration.SIXTEENTH),
                new Note(Tone.C, Duration.HALF)
            };Play(Major);
            Note[] Minor ={
                new Note(Tone.E, Duration.SIXTEENTH),
                new Note(Tone.E, Duration.SIXTEENTH),
                new Note(Tone.E, Duration.SIXTEENTH),
                new Note(Tone.C, Duration.EIGHTH),                
            }; Play(Minor);*/
            Note[] Start ={
                new Note(Tone.C, Duration.SIXTEENTH),
                new Note(Tone.E, Duration.SIXTEENTH),
                new Note(Tone.F, Duration.SIXTEENTH),
                new Note(Tone.G, Duration.EIGHTH),
            }; Beep.Play(Start);
            
        }
        static public string ConvertLocalDiskLetterToUNC(string Path, string Host)        {           return @"\\" + Host + @"\$" + Path.Substring(0, 1) + Path.Substring(2);         }

        private void bAddPCFromDomain_Click(object sender, EventArgs e)
        {
            if (WMI.IsDomainMember())
            {
                Debug.WriteLine(WMI.GetComputerDomainName());
            }
            else
            {
                Debug.WriteLine(WMI.GetComputerWorkgroupName());
                using (DirectoryEntry workgroup = new DirectoryEntry("WinNT://Workgroup"))
                {
                    foreach (DirectoryEntry child in workgroup.Children)
                    {
                        Debug.WriteLine(child.Name);
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.WriteSettings("Version","1");
            Config.WriteSettings("SourceFolder", tSourceCopy.Text);
            Config.WriteSettings("TargetFolder", tTargetCopy.Text);
            Config.WriteSettingsHosts(chkList_PC);            
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

