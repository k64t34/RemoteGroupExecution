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


        struct HostInfo
        {
            public string Name;
            public bool Ping;
            public bool Done;
            public HostInfo(string Name) { this.Name = Name.Trim(); this.Ping = false; this.Done = false; }
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
                try { Host.Ping = resultPing; }
                catch { }
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
                        if (!H.Done)
                        {
                            Task task = new Task(() => this.Ping(H)); task.Start();
                        }
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
            //public string Name;
            public int Count;
            //public int ID;
            //public FileInfo(string SourcePath){this.SourcePath = SourcePath; this.Name = SourcePath; this.TargetPath = ""; this.Count = 0;/*this.ID = -1; */}
            public FileInfo(string File)             {                this.Count = 0;                this.File = File; }
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
            String SourceFolder;
            String tmpFolder;
            String TargetFolder;
            public CopyFilesQueue(string SourceFolder, String TargetFolder/*, WebBrowser Browser*/,
                CheckedListBox chkList, CancellationToken cancellationToken,
                delegateCreateElement c,delegateUpdateElement u, delegateGetElement g
               )
            {
                this.SourceFolder = SourceFolder;
                this.cancellationToken = cancellationToken;                
                this.CreateElement=c;
                this.UpdateElement=u;
                this.GetElement=g;
                this.TargetFolder = TargetFolder;

                divFile = (HtmlElement)THIS.Invoke(CreateElement, "div");
                divFile.SetAttribute("id", "div.Files");                
                StringBuilder Output = new StringBuilder();
                GetFolder(SourceFolder.Length, SourceFolder, Output);
                divFile.InnerHtml = Output.ToString()+_BRLF;
                THIS.BeginInvoke(UpdateElement, divFile);

                divHost = (HtmlElement)THIS.Invoke(CreateElement, "div");
                divHost.SetAttribute("id", "div.Hosts");
                divHost.InnerHtml = HTMLTagSpan("Host list:", CSS_BaseHighLight) + _BRLF;
                GetHost(chkList);
                THIS.BeginInvoke(UpdateElement, divHost);
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

            void GetFolder(int BaseFolderLenght, string Folder, StringBuilder Output)
            {
                HTMLBlock1 HTMLblock;                
                try
                {
                    if (System.IO.Directory.Exists(Folder))
                    {
                        string[] files = System.IO.Directory.GetFiles(Folder);
                        int Count = File.Count;
                        string Path;
                        foreach (string s in files) 
                         {
                            if (cancellationToken.IsCancellationRequested) break;
                            Path = s.Substring(BaseFolderLenght);
                            this.File.Add(new FileInfo(Path));
                            HTMLblock = new HTMLBlock1("file." + Count);
                            HTMLblock.Label.InnerHtml(Path);
                            Count++;
                            Output.Append(HTMLblock.ToString());//                            divFile.InnerHtml += HTMLblock.ToString();//                                s + HTMLBuilder.TagBuilder._BR; 
                          }                        
                        string[] dirs = System.IO.Directory.GetDirectories(Folder);
                        foreach (string s in dirs) 
                        {
                            if (cancellationToken.IsCancellationRequested) break; 
                            GetFolder(BaseFolderLenght,s, Output); 
                        }
                    }
                }
                catch { }
            }

            //         public static void CopyFiles(string SourceFolder, String TargetFolder, /*HtmlDocument Document*/ WebBrowser Browser, CheckedListBox chkList, CancellationToken cancellationToken)
            //        { 
            //                try
            //                {
            //                    cancellationToken.ThrowIfCancellationRequested();
            //                    //Task task = new Task(() => HostQ.Pinging(cancellationToken)); task.Start();                
            //                    CopyFilesQueue CopyQ = new CopyFilesQueue(SourceFolder, TargetFolder, Browser, chkList, cancellationToken);
            //                    //*for (int f = 0; f != FilesQ.File.Count; f++)
            //                    //{ 
            //                    //}
            //                }
            //                catch (Exception e) {
            //                Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " Exeption: "+e.Message);
            //            }
            //#if DEBUG
            //                Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " CopyFiles finish");
            //#endif


            //        }
            public void Copy()
            {
                
                try
                {
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

                    for (int  idf=0; idf != File.Count; idf++)//foreach (FileInfo f in File)
                    {
                        if (cancellationToken.IsCancellationRequested) break;
                            if (!CashFile(idf))
                        {
                            divFile = (HtmlElement)THIS.Invoke(GetElement, "l.file." + idf);
                            divFile.InnerHtml += HTMLSpanFAULT();
                        }
                        else
                        {
                            for (int idh = 0; idh != Host.Count; idh++)//                                foreach (HostInfo h in Host)
                            {
                                if (cancellationToken.IsCancellationRequested) break;
                                divHost = (HtmlElement)THIS.Invoke(GetElement, "d.host." + idh);
                                StringBuilder Output = new StringBuilder();
                                Output.Append("Ping ");
                                bool local_result = false;
                                try
                                {
                                    if (Ping(Host[idh].Name)) local_result = true;                                    
                                }
                                catch {  }
                                if (!local_result) {
                                    Output.Append(HTMLSpanFAULT());
                                    divHost.InnerHtml += Output.ToString();
                                    divHost = (HtmlElement)THIS.Invoke(GetElement, "l.host." + idh);
                                    divHost.InnerHtml += HTMLSpanFAULT();
                                }
                                else
                                {
                                    Output.Append(HTMLSpanOK() + _BR);
                                    divHost.InnerHtml += Output.ToString();
                                    if (cancellationToken.IsCancellationRequested) break;
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
                    int LastSlash = File[ID].File.LastIndexOf("\\");
                    if (LastSlash != -1)
                    {
                        String SubDir = tmpFolder + File[ID].File.Substring(0, LastSlash);
                        if (!System.IO.Directory.Exists(SubDir))
                            try { System.IO.Directory.CreateDirectory(SubDir); }
                            catch { }
                    }
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        System.IO.File.Copy(System.IO.Path.Combine(SourceFolder, File[ID].File), System.IO.Path.Combine(tmpFolder, File[ID].File), true);
                        result = true;
                        divFile.InnerHtml += "Cash " + HTMLSpanOK();
                    }
                }
                catch (Exception e){divFile.InnerHtml += "Cash " + HTMLSpanFAULT() + " "+HTMLTagSpan(e.Message,CSS_BaseAlert)+" "+ SourceFolder + File[ID].File+" "+ tmpFolder + File[ID].File; }
                return result;

            }
        }
        
    }
}