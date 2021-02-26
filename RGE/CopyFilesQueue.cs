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
            public string SourcePath;
            public string TargetPath;
            public string Name;
            public int Count;
            public int ID;
            public FileInfo(string SourcePath){this.SourcePath = SourcePath; this.Name = SourcePath; this.TargetPath = ""; this.Count = 0;this.ID = -1; }
            public FileInfo(string SourcePath,int ID) { this.SourcePath = SourcePath; this.Name = SourcePath; this.TargetPath = ""; this.Count = 0; this.ID = ID; }
        }
        public class CopyFilesQueue
        {
            CancellationToken cancellationToken;
            //HtmlDocument Document;
            WebBrowser Browser;
            List<FileInfo> File = new List<FileInfo>();
            List<HostInfo> Host = new List<HostInfo>();
            //HtmlElement divHost;
            HtmlElement divFile;
            HtmlElement divHost;
            public delegateCreateElement CreateElement;
            public delegateUpdateElement UpdateElement;
            public delegateGetElement GetElement;
            String tmpFolder;
            public CopyFilesQueue(string SourceFolder, String TargetFolder/*, WebBrowser Browser*/,
                CheckedListBox chkList, CancellationToken cancellationToken,
                delegateCreateElement c,delegateUpdateElement u, delegateGetElement GetElement
               )
            {
                this.cancellationToken = cancellationToken;
                this.GetElement = GetElement;
                divFile = (HtmlElement)THIS.Invoke(c, "div");
                divFile.SetAttribute("id", "div.Files");
                StringBuilder Output = new StringBuilder();
                GetFolder(SourceFolder, Output);
                divFile.InnerHtml = Output.ToString();
                THIS.BeginInvoke(u, divFile);

                divHost = (HtmlElement)THIS.Invoke(c, "div");
                divHost.SetAttribute("id", "div.Hosts");
                GetHost(chkList);
                THIS.BeginInvoke(u, divHost);


            }
            void GetHost(CheckedListBox chkList)
            {
                HTMLBlock1 HTMLblock;
                StringBuilder Output = new StringBuilder();
                string s;
                foreach (int indexChecked in chkList.CheckedIndices)
                {
                    if (cancellationToken.IsCancellationRequested) break;                    
                    s = chkList.Items[indexChecked].ToString();
                    HTMLblock = new HTMLBlock1("HOST." + s);
                    HTMLblock.Label.InnerHtml(s);
                    Output.Append(HTMLblock.ToString());//                    divHost.InnerHtml += HTMLblock.ToString();// s + HTMLBuilder.TagBuilder._BR;
                    Host.Add(new HostInfo(s));
                }
                divHost.InnerHtml = Output.ToString();
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
                            this.File.Add(new FileInfo(s, Count));
                            HTMLblock = new HTMLBlock1("file." + Count);
                            HTMLblock.Label.InnerHtml(s);
                            Count++;
                            Output.Append(HTMLblock.ToString());//                            divFile.InnerHtml += HTMLblock.ToString();//                                s + HTMLBuilder.TagBuilder._BR; 
                          }                        
                        string[] dirs = System.IO.Directory.GetDirectories(Folder);
                        foreach (string s in dirs) 
                        {
                            if (cancellationToken.IsCancellationRequested) break; 
                            GetFolder(s,Output); 
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
                    String tmpFolder = Environment.GetEnvironmentVariable("tmp");
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
                    tmpFolder += @"\" + DateTime.Now.ToString("ddMMyyyyHHmmss")+".TMP"; 
                    System.IO.Directory.CreateDirectory(tmpFolder);

                    foreach (FileInfo f in File)
                    {
                        if (!CashFile(f.ID))
                        {
                            divFile = (HtmlElement)THIS.Invoke(GetElement, "l.file." + f.ID);
                            divFile.InnerHtml += HTMLSpanFAULT();
                        }
                        else
                        {
                            foreach (HostInfo h in Host)
                            {

                            }
                        }
                    }
                }
                catch { }
                finally
                {
                    if (System.IO.Directory.Exists(tmpFolder)) System.IO.Directory.Delete(tmpFolder);
                }
            }
            bool CashFile(int ID)
            {
                bool result = false;
                divFile = (HtmlElement)THIS.Invoke(GetElement, "d.file." + ID);
                try {
                    

                    //FileInfo f = File.Find(ID);
                    //if (f == null)
                    System.IO.File.Copy("", tmpFolder,true);
                    result = true;
                    divFile.InnerHtml += "Cash " + HTMLSpanOK();
                }
                catch (Exception e){divFile.InnerHtml += "Cash " + HTMLSpanFAULT() + " "+HTMLTagSpan(e.Message,CSS_BaseAlert);}
                return result;

            }
        }
        
    }
}