using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Text;



//namespace CopyFilesQueue
namespace RGE
{
    public partial class Form1
    {


        class HostInfo
        {
            public string Name;
            //public bool Ping;
            //public bool Done;
            public int Status=0; // 0-unknown;1-OK;2-FAIL
            public int FCount=-1;
              

public HostInfo(string Name) { this.Name = Name.Trim(); /*this.Ping = false;*//* this.Done = false; *//*this.Status = 0; this.FCount = -1;*/ }
        }

        class HostQueue
        {
            List<HostInfo> Host = new List<HostInfo>();
            int CountDone;
            void Ping(HostInfo Host)
            {
#if DEBUG
                Debug.WriteLine("\t\t\tThread " + Thread.CurrentThread.ManagedThreadId + " Ping start: " + Host.Name/*+ " FreeThreadCount="+ FreeThreadCount.ToString()*/);
#endif
                bool resultPing = false;
                try
                {
                    System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                    System.Net.NetworkInformation.PingReply reply = ping.Send(Host.Name);
                    if (reply.Status.ToString() == "Success") resultPing = true;
                }
                catch { }
                //try { Host.Ping = resultPing; }
                //catch { }
#if DEBUG
                Debug.WriteLine("\t\t\tThread " + Thread.CurrentThread.ManagedThreadId + "Ping End: " + Host.Name/*+ " FreeThreadCount="+ FreeThreadCount.ToString()*/);
#endif
            }
            public void Pinging(CancellationToken cancellationToken)
            {
#if DEBUG
                Debug.WriteLine("\tThread " + Thread.CurrentThread.ManagedThreadId + " Pinging start: ");
#endif
                //while (CountDone != 0)
                {
                    HostInfo H;
                    //bool resultPing;
                    for (int iH = 0; iH != Host.Count; iH++)
                    {
                        H = Host[iH];
                        if (cancellationToken.IsCancellationRequested) break;
                        //if (!H.Done)
                        //{
                        //    Task task = new Task(() => this.Ping(H)); task.Start();
                        //}
                    }
                    //if (cancellationToken.IsCancellationRequested) break;
                }
#if DEBUG
                Debug.WriteLine("\tThread " + Thread.CurrentThread.ManagedThreadId + " Pinging finish");
#endif
            }
            public HostQueue(CheckedListBox chkList, CancellationToken cancellationToken)
            {
#if DEBUG
                Debug.WriteLine("\tThread " + Thread.CurrentThread.ManagedThreadId + " HostQueue start: ");
#endif

                foreach (int indexChecked in chkList.CheckedIndices)
                {
                    if (cancellationToken.IsCancellationRequested) break;
                    Host.Add(new HostInfo(chkList.Items[indexChecked].ToString()));
                }
                CountDone = Host.Count;
#if DEBUG
                Debug.WriteLine("\tThread " + Thread.CurrentThread.ManagedThreadId + " HostQueue finish: \n\t Host count=" + Host.Count.ToString());
#endif
            }
        }
        struct FileInfo
        {
            //public string SourcePath;
            //public string TargetPath;
            public string File;
            public string Path;
            //public string Name;
            public int Count;
            //public int ID;
            //public FileInfo(string SourcePath){this.SourcePath = SourcePath; this.Name = SourcePath; this.TargetPath = ""; this.Count = 0;/*this.ID = -1; */}
            public FileInfo(string pFile,int pBaseFolderLenght=0)
            {
                this.Count = 0;                
                /*if (pBaseFolderLenght == 0){this.Path = pFile;}else{*/pFile= pFile.Substring(pBaseFolderLenght);/*}*/
                int LastSlash = pFile.LastIndexOf("\\");
                if (LastSlash == -1) { this.File = pFile; this.Path = ""; }
                else { LastSlash++; this.File = pFile.Substring(LastSlash);this.Path = pFile.Substring(0, LastSlash); }
            }
            
            //public FileInfo(string SourcePath,int ID) { this.SourcePath = SourcePath; this.Name = SourcePath; this.TargetPath = ""; this.Count = 0; this.ID = ID; }
        }
        public class CopyFilesQueue
        {
            CancellationToken cancellationToken;
            //HtmlDocument Document;
            //WebBrowser Browser;
            List<FileInfo> File = new List<FileInfo>();
            List<HostInfo> Host = new List<HostInfo>();
            //HtmlElement divHost;
            HtmlElement divFile;
            HtmlElement divHost;
            public delegateCreateElement CreateElement;
            public delegateUpdateElement UpdateElement;
            public delegateGetElement GetElement;
            public delegateProgressBar SetProgressBarMaximum;
            public delegateProgressBar SetProgressBarValue;
            String SourceFolder;
            String tmpFolder;
            String TargetFolder;
            int BaseFolderLenght;
            public CopyFilesQueue(string SourceFld, String TargetFld/*, WebBrowser Browser*/,
                CheckedListBox chkList, CancellationToken cancellationToken,
                delegateCreateElement c,delegateUpdateElement u, delegateGetElement g, delegateProgressBar PM, delegateProgressBar PV
               )
            {
                this.SetProgressBarMaximum = PM;
                this.SetProgressBarValue = PV;
                this.SourceFolder = SourceFld;
                this.cancellationToken = cancellationToken;                
                this.CreateElement=c;
                this.UpdateElement=u;
                this.GetElement=g;
                TargetFld = TargetFld.Trim();
                if (!TargetFld.EndsWith("\\")) TargetFld+= "\\";
                this.TargetFolder = @"\"+ TargetFld.Substring(0, 1)+ @"$" + TargetFld.Substring(2);

                BaseFolderLenght = SourceFolder.Length;

                divFile = (HtmlElement)THIS.Invoke(CreateElement, "div");
                divFile.SetAttribute("id", "div.Files");                
                StringBuilder Output = new StringBuilder();
                GetFolder(SourceFolder, Output);
                Output.Append("Total " + File.Count + " files<br>");
                divFile.InnerHtml = Output.ToString()+_BRLF;
                THIS.BeginInvoke(UpdateElement, divFile);

                divHost = (HtmlElement)THIS.Invoke(CreateElement, "div");
                divHost.SetAttribute("id", "div.Hosts");
                divHost.InnerHtml = HTMLTagSpan("Host list:", CSS_BaseHighLight) + _BRLF;
                GetHost(chkList);
                THIS.BeginInvoke(UpdateElement, divHost);
                THIS.BeginInvoke(UpdateElement, divFile);
                Host.ForEach(h => h.FCount = File.Count);
                THIS.BeginInvoke(SetProgressBarMaximum,File.Count*Host.Count);

            }
            void GetHost(CheckedListBox chkList)
            {
                HTMLBlock1 HTMLblock;
                StringBuilder Output = new StringBuilder();
                string s;
                int Counter = 0;
                foreach (int indexChecked in chkList.CheckedIndices)                    
                    {
                    if (cancellationToken.IsCancellationRequested) break;                    
                    s = chkList.Items[indexChecked].ToString();
                    HTMLblock = new HTMLBlock1("HOST." + Counter);
                    HTMLblock.Label.InnerHtml(s);                    
                    Output.Append(HTMLblock.ToString());//                    divHost.InnerHtml += HTMLblock.ToString();// s + HTMLBuilder.TagBuilder._BR;
                    this.Host.Add(new HostInfo(s));
                    Counter++;
                }
                divHost.InnerHtml+= Output.ToString();
                //CountDone = Host.Count;
            }

            void GetFolder(string Folder, StringBuilder Output)
            {
                HTMLBlock1 HTMLblock;                
                try
                {
                    if (System.IO.Directory.Exists(Folder))
                    {
                        string[] files = System.IO.Directory.GetFiles(Folder);
                        int Count = File.Count;                        
                        foreach (string s in files) 
                         {
                            if (cancellationToken.IsCancellationRequested) break;                            
                            this.File.Add(new FileInfo(s,BaseFolderLenght));
                            HTMLblock = new HTMLBlock1("file." + Count);
                            HTMLblock.Label.InnerHtml(File[Count].Path+File[Count].File);
                            Count++;
                            Output.Append(HTMLblock.ToString());//                            divFile.InnerHtml += HTMLblock.ToString();//                                s + HTMLBuilder.TagBuilder._BR; 
                          }                        
                        string[] dirs = System.IO.Directory.GetDirectories(Folder);
                        foreach (string s in dirs) 
                        {
                            if (cancellationToken.IsCancellationRequested) break; 
                            GetFolder(s, Output); 
                        }
                    }
                }
                catch { }
            }   
            public void Copy()
            {                
                try
                {
                    //TODO: Если исходные файлв не на локальном ПК
                    #region Temporary folder
                    tmpFolder = Environment.GetEnvironmentVariable("tmp");
                    if (String.IsNullOrEmpty(tmpFolder))
                    {
                        tmpFolder = Environment.GetEnvironmentVariable("temp");
                        if (String.IsNullOrEmpty(tmpFolder))
                        {
                            tmpFolder = Environment.GetEnvironmentVariable("HOMEDRIVE");
                            if (String.IsNullOrEmpty(tmpFolder))
                            {
                                tmpFolder = "C:"; 
                            }
                            tmpFolder +=  @"\TMP";                            
                        }
                    }
                    tmpFolder += @"\" + DateTime.Now.ToString("ddMMyyyyHHmmss")+".TMP\\";
                    HtmlElement htmltmpfolder = (HtmlElement)THIS.Invoke(CreateElement, "span");
                    htmltmpfolder.SetAttribute("id","tmpFolder");
                    htmltmpfolder.InnerHtml = HTMLTagSpan("Temporary folder:", CSS_BaseHighLight) + tmpFolder;
                    try
                    {
                        System.IO.Directory.CreateDirectory(tmpFolder);
                    }
                    catch { htmltmpfolder.InnerHtml += HTMLSpanFAULT() + " " + HTMLTagSpan("Failed to create temporary folder " + tmpFolder, CSS_BaseAlert); }
                    finally { THIS.Invoke(UpdateElement, htmltmpfolder); }
                    if (!System.IO.Directory.Exists(tmpFolder)) { throw new System.Exception("Failed to create temporary folder " + tmpFolder); }
                    #endregion

                    for (int  idf=0; idf != File.Count; idf++)//foreach (FileInfo f in File)
                    {
                        if (cancellationToken.IsCancellationRequested) break;
                            if (!CashFile(idf))
                        {
                            divFile = (HtmlElement)THIS.Invoke(GetElement, "l.file." + idf);
                            divFile.InnerHtml += HTMLSpanFAULT();
                            THIS.BeginInvoke(SetProgressBarValue, Host.Count);
                        }
                        else
                        {
                            for (int idh = 0; idh != Host.Count; idh++)//                                foreach (HostInfo h in Host)
                            {
                                THIS.BeginInvoke(SetProgressBarValue,1);
                                if (cancellationToken.IsCancellationRequested) break;
                                divHost = (HtmlElement)THIS.Invoke(GetElement, "d.host." + idh);
                                StringBuilder Output = new StringBuilder();                                
                                bool local_result = false;
                                //try{if (Ping(Host[idh].Name))local_result = true;}catch {}
                                //if (!local_result) {
                                //    Output.Append("Ping "+HTMLSpanFAULT()+_BRLF);
                                //    divHost.InnerHtml += Output.ToString();
                                //    divHost = (HtmlElement)THIS.Invoke(GetElement, "l.host." + idh);
                                //    divHost.InnerHtml += HTMLSpanFAULT();
                                //    continue;
                                //}                                                                                              
                                String FullTargetFolder = @"\\" + Host[idh].Name + this.TargetFolder+File[idf].Path;
                                Output.Append("Copy " + this.SourceFolder + File[idf].Path + File[idf].File + " to " + FullTargetFolder + File[idf].File);
                                if (!System.IO.Directory.Exists(FullTargetFolder))
                                {
                                    local_result = false;
                                    try
                                    {
                                        System.IO.Directory.CreateDirectory(FullTargetFolder);
                                        local_result = System.IO.Directory.Exists(FullTargetFolder);
                                    }
                                    catch (Exception e) { Output.Append(HTMLSpanFAULT() + HTMLTagSpan(e.Message,CSS_BaseAlert) + _BRLF); }
                                    if (!local_result)
                                    {
                                        divHost.InnerHtml += Output.ToString();
                                        if (Host[idh].Status == 0)
                                        {                                            
                                            divHost = (HtmlElement)THIS.Invoke(GetElement, "l.host." + idh);
                                            divHost.InnerHtml += HTMLSpanFAULT();
                                            Host[idh].Status = 2;
                                        }                                        
                                        continue;
                                    }
                                }
                                local_result = false;
                                try
                                  { System.IO.File.Copy(tmpFolder + File[idf].Path + File[idf].File,FullTargetFolder + File[idf].File,true)                                        ;
                                    local_result = true;
                                    Output.Append(HTMLSpanOK());                                    
                                }
                                catch (Exception e) { Output.Append(HTMLSpanFAULT() + HTMLTagSpan(e.Message, CSS_BaseAlert)); }
                                Output.Append(_BRLF);
                                divHost.InnerHtml += Output.ToString();
                                if (!local_result)
                                {
                                    if (Host[idh].Status == 0)
                                    {
                                        divHost = (HtmlElement)THIS.Invoke(GetElement, "l.host." + idh);
                                        divHost.InnerHtml += HTMLSpanFAULT();
                                        Host[idh].Status = 2;
                                    }

                                }
                                else
                                {
                                    Host[idh].FCount--;
                                    if (Host[idh].FCount == 0 && Host[idh].Status == 0) 
                                    {
                                        divHost = (HtmlElement)THIS.Invoke(GetElement, "l.host." + idh);
                                        divHost.InnerHtml += HTMLSpanOK();
                                        Host[idh].Status = 1;
                                    }
                                }
                            }
                        }
                    }
                }
                catch { }
                finally
                {
                    if (System.IO.Directory.Exists(tmpFolder))
                    {
                        HtmlElement htmltmpfoler = (HtmlElement)THIS.Invoke(GetElement, "tmpFolder");
                        htmltmpfoler.InnerHtml += " delete";
                        System.IO.Directory.Delete(tmpFolder,true);                        
                        if (System.IO.Directory.Exists(tmpFolder)) htmltmpfoler.InnerHtml +=HTMLSpanFAULT();
                        else htmltmpfoler.InnerHtml += HTMLSpanOK();
                    }
                }
            }
            bool CashFile(int ID)
            {
                bool result = false;
                divFile = (HtmlElement)THIS.Invoke(GetElement, "d.file." + ID);
                try {
                    String tmpDir = tmpFolder + File[ID].Path;                    
                    if (!System.IO.Directory.Exists(tmpDir))
                        try { System.IO.Directory.CreateDirectory(tmpDir); }
                        catch { }                    
                    if (!cancellationToken.IsCancellationRequested)
                    {                        
                        System.IO.File.Copy(SourceFolder+ File[ID].Path + File[ID].File, System.IO.Path.Combine(tmpDir, File[ID].File), true);
                        result = true;
                        divFile.InnerHtml += "Cash " + HTMLSpanOK();
                    }
                }
                catch (Exception e){divFile.InnerHtml += "Cash " + HTMLSpanFAULT() + " "+HTMLTagSpan(e.Message,CSS_BaseAlert)+" "+ SourceFolder + File[ID].Path + File[ID].File + " "+ tmpFolder; }
                return result;

            }
        }
        
    }
}