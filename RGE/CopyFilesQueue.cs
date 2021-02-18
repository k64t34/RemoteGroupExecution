using System;
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
                bool resultPing;
                for (int iH=0;iH!=Host.Count;iH++)                
                {
                    H = Host[iH];
                    if (cancellationToken.IsCancellationRequested) break;
                    if (!H.Done) 
                    {
                        Task task = new Task(() => this.Ping(H)); task.Start();

                        /* 
                         * resultPing = false;
                         * try
                          {
                              System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                              System.Net.NetworkInformation.PingReply reply = ping.Send(H.Name);
                              if (reply.Status.ToString() == "Success") resultPing = true;
                          }
                          catch {}
                      H.Ping = resultPing;*/
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
            CountDone = 0;
            foreach (int indexChecked in chkList.CheckedIndices)
            {
                if (cancellationToken.IsCancellationRequested) break;                    
                Host.Add(new HostInfo (chkList.Items[indexChecked].ToString()));
                CountDone++;
            }
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
        List<FileInfo> File = new List<FileInfo>();
        int CountDone;

        CopyFilesQueue(string SourceFolder) 
        {
            GetFolder(SourceFolder);            
        }
        void GetFolder(string Folder) 
        {
            try
            {
                if (System.IO.Directory.Exists(Folder))
                {
                    //GetFiles(path)
                    string[] files = System.IO.Directory.GetFiles(Folder);
                    foreach (string s in files)
                    {
                        File.Add(new FileInfo(s));
                    }
                    //GetDirectories
                    string[] dirs = System.IO.Directory.GetDirectories(Folder);
                    foreach (string s in dirs)
                    {
                        GetFolder(s);
                    }                    
                }
            }
            catch { }
        }


        public static void CopyFiles(String SourceFolder, String TargetFolder, Form1 UIForm, CancellationToken cancellationToken)
        {
#if DEBUG
            Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " CopyFiles start: " );
#endif
            cancellationToken.ThrowIfCancellationRequested();
            HostQueue HostQ = new HostQueue(UIForm.chkList_PC, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            Task task =new Task(() => HostQ.Pinging(cancellationToken));task.Start();
            CopyFilesQueue FilesQ = new CopyFilesQueue(SourceFolder);
            Thread.Sleep(3000);
#if DEBUG
            Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " CopyFiles finish: ");
#endif
        }
    }    
}
