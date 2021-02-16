//https://www.abygeorgea.com/blog/2018/09/08/running-command-line-in-remote-machine-using-wmi/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Threading;
using System.Configuration;
using System.Diagnostics;

public class ProcessWMI
{
    const string ROOT_CIMv2 = @"\ROOT\CIMV2";
    public uint ProcessId;
    public int ExitCode;
    public bool EventArrived;
    public ManualResetEvent mre = new ManualResetEvent(false);
    public void ProcessStoptEventArrived(object sender, EventArrivedEventArgs e)
    {
        if ((uint)e.NewEvent.Properties["ProcessId"].Value == ProcessId)
        {
            Console.WriteLine("Process: {0}, Stopped with Code: {1}", (int)(uint)e.NewEvent.Properties["ProcessId"].Value, (int)(uint)e.NewEvent.Properties["ExitStatus"].Value);
            ExitCode = (int)(uint)e.NewEvent.Properties["ExitStatus"].Value;
            EventArrived = true;
            mre.Set();
        }
    }
    public ProcessWMI()
    {
        this.ProcessId = 0;
        ExitCode = -1;
        EventArrived = false;
    }
    public void ExecuteRemoteProcessWMI(string remoteComputerName, string arguments, int WaitTimePerCommand)
    {
        string strUserName = string.Empty;
        try
        {
            ConnectionOptions connOptions = new ConnectionOptions();
            //Note: This will connect  using below credentials. If not provided, it will be based on logged in user
            connOptions.Username = ConfigurationManager.AppSettings["RemoteMachineLogonUser"];
            connOptions.Password = ConfigurationManager.AppSettings["RemoteMachineUserPassword"];

            connOptions.Impersonation = ImpersonationLevel.Impersonate;
            connOptions.EnablePrivileges = true;
            ManagementScope manScope = new ManagementScope(@"\\"+ remoteComputerName + ROOT_CIMv2, connOptions);

            try
            {
                manScope.Connect();
            }
            catch (Exception e)
            {
                throw new Exception("Management Connect to remote machine " + remoteComputerName + " as user " + strUserName + " failed with the following error " + e.Message);
            }
            ObjectGetOptions objectGetOptions = new ObjectGetOptions();
            ManagementPath managementPath = new ManagementPath("Win32_Process");
            using (ManagementClass processClass = new ManagementClass(manScope, managementPath, objectGetOptions))
            {
                using (ManagementBaseObject inParams = processClass.GetMethodParameters("Create"))
                {
                    inParams["CommandLine"] = arguments;
                    using (ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null))
                    {

                        if ((uint)outParams["returnValue"] != 0)
                        {
                            throw new Exception("Error while starting process " + arguments + " creation returned an exit code of " + outParams["returnValue"] + ". It was launched as " + strUserName + " on " + remoteComputerName);
                        }
                        this.ProcessId = (uint)outParams["processId"];
                    }
                }
            }

            SelectQuery CheckProcess = new SelectQuery("Select * from Win32_Process Where ProcessId = " + ProcessId);
            using (ManagementObjectSearcher ProcessSearcher = new ManagementObjectSearcher(manScope, CheckProcess))
            {
                using (ManagementObjectCollection MoC = ProcessSearcher.Get())
                {
                    if (MoC.Count == 0)
                    {
                        throw new Exception("ERROR AS WARNING: Process " + arguments + " terminated before it could be tracked on " + remoteComputerName);
                    }
                }
            }

            WqlEventQuery q = new WqlEventQuery("Win32_ProcessStopTrace");
            using (ManagementEventWatcher w = new ManagementEventWatcher(manScope, q))
            {
                w.EventArrived += new EventArrivedEventHandler(this.ProcessStoptEventArrived);
                w.Start();
                if (!mre.WaitOne(WaitTimePerCommand, false))
                {
                    w.Stop();
                    this.EventArrived = false;
                }
                else
                    w.Stop();
            }
            if (!this.EventArrived)
            {
                SelectQuery sq = new SelectQuery("Select * from Win32_Process Where ProcessId = " + ProcessId);
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(manScope, sq))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        queryObj.InvokeMethod("Terminate", null);
                        queryObj.Dispose();
                        throw new Exception("Process " + arguments + " timed out and was killed on " + remoteComputerName);
                    }
                }
            }
            else
            {
                if (this.ExitCode != 0)
                    throw new Exception("Process " + arguments + "exited with exit code " + this.ExitCode + " on " + remoteComputerName + " run as " + strUserName);
                else
                    Console.WriteLine("process exited with Exit code 0");
            }

        }
        catch (Exception e)
        {
            throw new Exception(string.Format("Execute process failed Machinename {0}, ProcessName {1}, RunAs {2}, Error is {3}, Stack trace {4}", remoteComputerName, arguments, strUserName, e.Message, e.StackTrace), e);
        }
    }
    static public string GetRemoteEnvironmentVariable(string remoteComputerName,string varName)
    {
        string result="";
        try
        {
            ConnectionOptions connOptions = new ConnectionOptions();
            //Note: This will connect  using below credentials. If not provided, it will be based on logged in user
            connOptions.Username = ConfigurationManager.AppSettings["RemoteMachineLogonUser"];
            connOptions.Password = ConfigurationManager.AppSettings["RemoteMachineUserPassword"];

            connOptions.Impersonation = ImpersonationLevel.Impersonate;
            connOptions.EnablePrivileges = true;
            ManagementScope manScope = new ManagementScope(@"\\" + remoteComputerName + ROOT_CIMv2, connOptions);
            manScope.Connect();
            SelectQuery q = new SelectQuery("Win32_Environment");
            ManagementObjectSearcher query =  new ManagementObjectSearcher(manScope, q, null);
            ManagementObjectCollection queryCollection = queryCollection = query.Get();
            Debug.WriteLine("queryCollection Count =" + queryCollection.Count);

            //TODO: Try start  “Remote Procedure Call (RPC) Locator” and “Remote Procedure Call (RPC)” service should be start on remote machine.
            //– To start above services, type services.msc on run prompt and search for above services. Right click on each service and set it to automatic and start it.
            varName = varName.Trim();            
            foreach (ManagementObject envVar in queryCollection)
            {
                if (envVar["Name"].ToString() == varName)
                {   
                    result = envVar["VariableValue"].ToString();            
                    break;
                }
            }                        
        }
        catch {  }
        return result;
    }
}





/* Origin https://www.abygeorgea.com/blog/2018/09/08/running-command-line-in-remote-machine-using-wmi/
  
  
ProcessWMI p = new ProcessWMI();
p.ExecuteRemoteProcessWMI(remoteMachine, sBatFile, timeout);
 
public class ProcessWMI
{
    public uint ProcessId;
    public int ExitCode;
    public bool EventArrived;
    public ManualResetEvent mre = new ManualResetEvent(false);
    public void ProcessStoptEventArrived(object sender, EventArrivedEventArgs e)
    {
        if ((uint)e.NewEvent.Properties["ProcessId"].Value == ProcessId)
        {
            Console.WriteLine("Process: {0}, Stopped with Code: {1}", (int)(uint)e.NewEvent.Properties["ProcessId"].Value, (int)(uint)e.NewEvent.Properties["ExitStatus"].Value);
            ExitCode = (int)(uint)e.NewEvent.Properties["ExitStatus"].Value;
            EventArrived = true;
            mre.Set();
        }
    }
    public ProcessWMI()
    {
        this.ProcessId = 0;
        ExitCode = -1;
        EventArrived = false;
    }
    public void ExecuteRemoteProcessWMI(string remoteComputerName, string arguments, int WaitTimePerCommand)
    {
        string strUserName = string.Empty;
        try
        {
            ConnectionOptions connOptions = new ConnectionOptions();
            //Note: This will connect  using below credentials. If not provided, it will be based on logged in user
             connOptions.Username = ConfigurationManager.AppSettings["RemoteMachineLogonUser"];
             connOptions.Password = ConfigurationManager.AppSettings["RemoteMachineUserPassword"];

            connOptions.Impersonation = ImpersonationLevel.Impersonate;
            connOptions.EnablePrivileges = true;
            ManagementScope manScope = new ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", remoteComputerName), connOptions);

            try
            {
                manScope.Connect();
            }
            catch (Exception e)
            {
                throw new Exception("Management Connect to remote machine " + remoteComputerName + " as user " + strUserName + " failed with the following error " + e.Message);
            }
            ObjectGetOptions objectGetOptions = new ObjectGetOptions();
            ManagementPath managementPath = new ManagementPath("Win32_Process");
            using (ManagementClass processClass = new ManagementClass(manScope, managementPath, objectGetOptions))
            {
                using (ManagementBaseObject inParams = processClass.GetMethodParameters("Create"))
                {
                    inParams["CommandLine"] = arguments;
                    using (ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null))
                    {

                        if ((uint)outParams["returnValue"] != 0)
                        {
                            throw new Exception("Error while starting process " + arguments + " creation returned an exit code of " + outParams["returnValue"] + ". It was launched as " + strUserName + " on " + remoteComputerName);
                        }
                        this.ProcessId = (uint)outParams["processId"];
                    }
                }
            }

            SelectQuery CheckProcess = new SelectQuery("Select * from Win32_Process Where ProcessId = " + ProcessId);
            using (ManagementObjectSearcher ProcessSearcher = new ManagementObjectSearcher(manScope, CheckProcess))
            {
                using (ManagementObjectCollection MoC = ProcessSearcher.Get())
                {
                    if (MoC.Count == 0)
                    {
                        throw new Exception("ERROR AS WARNING: Process " + arguments + " terminated before it could be tracked on " + remoteComputerName);
                    }
                }
            }

            WqlEventQuery q = new WqlEventQuery("Win32_ProcessStopTrace");
            using (ManagementEventWatcher w = new ManagementEventWatcher(manScope, q))
            {
                w.EventArrived += new EventArrivedEventHandler(this.ProcessStoptEventArrived);
                w.Start();
                if (!mre.WaitOne(WaitTimePerCommand,false))
                {
                    w.Stop();
                    this.EventArrived = false;
                }
                else
                    w.Stop();
            }
            if (!this.EventArrived)
            {
                SelectQuery sq = new SelectQuery("Select * from Win32_Process Where ProcessId = " + ProcessId);
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(manScope, sq))
                {
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        queryObj.InvokeMethod("Terminate", null);
                        queryObj.Dispose();
                        throw new Exception("Process " + arguments + " timed out and was killed on " + remoteComputerName);
                    }
                }
            }
            else
            {
                if (this.ExitCode != 0)
                    throw new Exception("Process " + arguments + "exited with exit code " + this.ExitCode + " on " + remoteComputerName + " run as " + strUserName);
                else
                    Console.WriteLine("process exited with Exit code 0");
            }

        }
        catch (Exception e)
        {
            throw new Exception(string.Format("Execute process failed Machinename {0}, ProcessName {1}, RunAs {2}, Error is {3}, Stack trace {4}", remoteComputerName, arguments, strUserName, e.Message, e.StackTrace), e);
        }
    }
} 
 
 */





/*
 Update Environment variable on remote machine using Management Object in C# 
 https://ramveersingh.wordpress.com/2011/05/30/update-environment-variable-on-remote-machine-using-management-object-in-c/

Update Environment variable on remote machine using Management Object in C#
May 30, 2011 — RAM
Following is the code to update environment variable in remote machine. By using this method we are not able to add new environment variable but we can update existing environment variable.

/// <param name="MachineName">Remote machine which environment variable need to update.</param>
/// <param name="username">username of remot machine which is use for login </param>
/// <param name="password">password of remote machine which is use for login</param>
/// <param name="VariableName">Varaible name which need to be update</param>
/// <param name="VariableValue">new value of varialbe</param>
public void UpdateEnvironmentVariable(string MachineName, string username, string password, string VariableName, string VariableValue)
{
    string ResultInfo = "";
    bool IsFound = false;
    ManagementObjectSearcher query = null;
    ManagementObjectCollection queryCollection = null;
 
    ConnectionOptions opt = new ConnectionOptions();
 
    opt.Impersonation = ImpersonationLevel.Impersonate;
    if (!chkLocal.Checked)
    {
        opt.EnablePrivileges = true;
        opt.Username = username;
        opt.Password = password;
        opt.Impersonation = ImpersonationLevel.Impersonate;
        opt.EnablePrivileges = true;
    }
    try
    {
        ManagementPath p = new ManagementPath("\\\\" + MachineName + "\\root\\cimv2");
 
        ManagementScope msc = new ManagementScope(p, opt);
 
        SelectQuery q = new SelectQuery("Win32_Environment");
 
        query = new ManagementObjectSearcher(msc, q, null);
        queryCollection = query.Get();
 
     
 
        ResultInfo += "\r\nTotal Count - " + queryCollection.Count;
        foreach (ManagementObject envVar in queryCollection)
        {
            if (envVar["Name"].ToString() == VariableName.Trim())
            {
                ResultInfo += "\r\n" + "System environment variable " + envVar["Name"] + " = " + envVar["VariableValue"];
                string OldValue = envVar["VariableValue"].ToString();
                envVar.SetPropertyValue("VariableValue", VariableValue.Trim());
                envVar.Put();
                IsFound = true;
                ResultInfo += "\r\n" + "Message:\tEnvironment variable update successfully";
                ResultInfo += "\r\n" + "OldValue: " + OldValue;
                ResultInfo += "\r\n" + "NewValue: " + VariableValue.Trim();
                break;
            }
        }
        if (IsFound == false)
            ResultInfo += "\r\nNot Found:\t" + txtVariableName.Text.Trim() + " not found in environment vairable";
    }
    catch (ManagementException Ex)
    {
        ResultInfo += "\r\nError:\t" + Ex.Message;
        ResultInfo += "\r\nStack:\t" + Ex.StackTrace;
    }
    catch (System.UnauthorizedAccessException Ex)
    {
        ResultInfo += "\r\nError:\t" + Ex.Message;
        ResultInfo += "\r\nStack:\t" + Ex.StackTrace;
    }
    catch (Exception Ex)
    {
        ResultInfo += "\r\nError:\t" + Ex.Message;
        ResultInfo += "\r\nStack:\t" + Ex.StackTrace;
    }
}
Folloiwng are some points to run above code successfully –
1. “Remote Procedure Call (RPC) Locator” and “Remote Procedure Call (RPC)” service should be start on remote machine.
– To start above services, type services.msc on run prompt and search for above services. Right click on each service and set it to automatic and start it.

2. Window Firewall should be configure or should be off on remote machine.

3. A user (which will access registry of remote machine) must be added with administrator privileges on remote machine. To add user with administrator privileges do following thing
– Right click on MyComputer and click on manage.
– Expand Local users and groups
– Double click on groups and then double click on administrator group.
– Add user (which will access registry of remote machine from another machine) in this group.
 
 */