﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



//namespace CopyFilesQueue
namespace RGE
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
            try{                Host.Ping = resultPing;            }
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
                for (int iH=0;iH!=Host.Count;iH++)                
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
                Host.Add(new HostInfo (chkList.Items[indexChecked].ToString()));                
            }
            CountDone = Host.Count;
#if DEBUG
            Debug.WriteLine("\tThread " + Thread.CurrentThread.ManagedThreadId + " HostQueue finish: \n\t Host count="+Host.Count.ToString());
#endif
        }
    }
    struct FileInfo
    {
        public string SourcePath;
        public string TargetPath;
        public string Name;
        public int Count;
        public FileInfo(string SourcePath) { this.SourcePath = SourcePath; this.Name = SourcePath; this.TargetPath = ""; this.Count = 0;
#if DEBUG
            Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " Add Files" + SourcePath);
#endif
        }
    }
    class CopyFilesQueue
    {
        CancellationToken cancellationToken;
        //HtmlDocument Document;
        WebBrowser Browser;
        List<FileInfo> File = new List<FileInfo>();
        List<HostInfo> Host = new List<HostInfo>();
        //HtmlElement divHost;
        HtmlElement divFile;
        HtmlElement divHost;


        CopyFilesQueue(string SourceFolder, String TargetFolder, WebBrowser B, CheckedListBox chkList, CancellationToken cancellationToken) 
        {
            this.Browser = B;
            this.cancellationToken = cancellationToken;

            //if (Browser.InvokeRequired){Browser.Invoke(new Action(() =>{divFile = Browser.Document.CreateElement("<div>");}));}
            //else{ divFile = Browser.Document.CreateElement("<div>"); }
            Browser.Invoke((MethodInvoker)delegate
            {
                divFile = Browser.Document.CreateElement("<div>");
            });
            

            divFile.InnerHtml = "<br>sdfg<br>";
            divFile.SetAttribute("id", "div.Files");
            GetFolder(SourceFolder);


            Browser.Invoke((MethodInvoker)delegate
            {                
                Browser.Document.Body.AppendChild(divFile);
            });


            //if (Browser.InvokeRequired) { Browser.Invoke(new Action(() => { Browser.Document.Body.AppendChild(divFile); })); }
            //else { Browser.Document.Body.AppendChild(divFile); }
            if (Browser.InvokeRequired) { Browser.Invoke(new Action(() => { divHost = Browser.Document.CreateElement("<div>"); })); }
            else { divHost = Browser.Document.CreateElement("<div>"); }
            GetHost(chkList);
            if (Browser.InvokeRequired) { Browser.Invoke(new Action(() => { Browser.Document.Body.AppendChild(divHost); })); }
            else { Browser.Document.Body.AppendChild(divHost); }

        }
        void GetHost(CheckedListBox chkList)
        {
            foreach (int indexChecked in chkList.CheckedIndices)
            {
                if (cancellationToken.IsCancellationRequested) break;
                Host.Add(new HostInfo(chkList.Items[indexChecked].ToString()));
            }
            //CountDone = Host.Count;
        }
        
        void GetFolder(string Folder) 
        {            
            try
            {
                if (System.IO.Directory.Exists(Folder))
                {                    
                    string[] files = System.IO.Directory.GetFiles(Folder);
                    foreach (string s in files){this.File.Add(new FileInfo(s)); divFile.InnerHtml += s + HTMLBuilder.TagBuilder._BR; }                    
                    string[] dirs = System.IO.Directory.GetDirectories(Folder);
                    foreach (string s in dirs){GetFolder(s);}                    
                }                
            }
            catch { }
        }

         public static void CopyFiles(string SourceFolder, String TargetFolder, /*HtmlDocument Document*/ WebBrowser Browser, CheckedListBox chkList, CancellationToken cancellationToken)
        { 
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    //Task task = new Task(() => HostQ.Pinging(cancellationToken)); task.Start();                
                    CopyFilesQueue CopyQ = new CopyFilesQueue(SourceFolder, TargetFolder, Browser, chkList, cancellationToken);
                    //*for (int f = 0; f != FilesQ.File.Count; f++)
                    //{ 
                    //}

                }
                catch (Exception e) {
                Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " Exeption: "+e.Message);
            }
#if DEBUG
                Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " CopyFiles finish");
#endif
            
            
        }
    }    
}
