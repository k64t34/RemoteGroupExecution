using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Management;
using System.Management.Instrumentation;

namespace RGE
{
    public partial class Form1
    {      
        async void Run_Copy()
        {
            await Task.Run(() =>
            {
            //try {
                StringBuilder Output = new StringBuilder();
                Output.Append(HTMLTagSpan(" Copy from:", CSS_BaseHighLight) + tSourceCopy.Text);
                if (!Directory.Exists(tSourceCopy.Text))
                {
                    Output.Append(HTMLTagSpan(" Folder doesn't exist", CSS_FAULT) + _BRLF);
                    WriteLog(Output);
                    goto Run_Copy_End_Run;                //return;
                }
                Output.Append(HTMLSpanOK() + _BRLF);
                Output.Append(HTMLTagSpan("Copy to  :", CSS_BaseHighLight) + tTargetCopy.Text + _BRLF);
                Output.Append(HTMLTagSpan("Override:", CSS_BaseHighLight) + chkCopyOverride.Checked + _BRLF);
                if (chkCopyOverride.Checked) Output.Append(HTMLTagSpan("Only newer:", CSS_BaseHighLight) + chkCopyOnlyNewer.Checked + _BRLF);
                Output.Append(HTMLTagSpan("Hosts:", CSS_BaseHighLight) + _BRLF);
                RunningThreadCount = 0;
                foreach (int indexChecked in chkList_PC.CheckedIndices)
                {
                    Output.Append(String.Format("{0}.{1}<br>\n", ++RunningThreadCount, chkList_PC.Items[indexChecked].ToString()));
                }
                Output.Append(_BRLF);
                WriteLog(Output);

                RunningThreadCount = 0;
                Task task;
                foreach (int indexChecked in chkList_PC.CheckedIndices)
                {
                    if (MainStatus == 0) { break; }
                    //task = new Task(() => Do_Copy(indexChecked, cts.Token));
                    //task.Start();
                    //Thread.Sleep(1);
                }
                Thread.Sleep(1000);
                while (RunningThreadCount != 0)
                {
#if DEBUG
                    Debug.WriteLine("Wait Thread finish: " + RunningThreadCount.ToString());
#endif
                    Thread.Sleep(1000);
                }
                WriteLog(_BRLF);
#if DEBUG
                Debug.WriteLine("All Thread finished");
#endif
            Run_Copy_End_Run: End_Run();
            });
        //}
        //catch {}
        }
//        //************************************************************
//        static void Do_Copy(int Index, CancellationToken cancellationToken) {
//            //************************************************************
//            RunningThreadCount++;
//            bool result = false;
//            string Host = THIS.chkList_PC.Items[(int)Index].ToString();
//#if DEBUG
//            Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " start: " + Host/*+ " FreeThreadCount="+ FreeThreadCount.ToString()*/);
//#endif
//            var block = new PCHTMLBlock(Host);
//            try
//            {
//                cancellationToken.ThrowIfCancellationRequested();
//                block.Div.Add("Ping ");
//                bool local_result = false;
//                try
//                {
//                    if (Ping(Host)) local_result = true;
//                }
//                catch { }
//                if (!local_result)
//                    block.Div.Add(HTMLSpanFAULT());
//                else
//                {
//                    block.Div.Add(HTMLSpanOK() + _BRLF);
//                    cancellationToken.ThrowIfCancellationRequested();
//                    local_result = false;
//                    var subBlock = new PCHTMLBlock(Host + ".CopyScript");
//                    subBlock.Label.InnerHtml("Copy script to remote host ");
//                    String Source = Path.Combine(ScriptFolder, THIS.tScriptFile.Text);
//                    String Target = Path.Combine(@"\\" + Host + @"\c$\Windows\temp", THIS.tScriptFile.Text);
//                    String DateTimeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
//                    String LogFile = Target + "." + DateTimeStamp + ".log";
//                    subBlock.Div.Add("Copy " + Source + " to " + Target);
//                    try
//                    {
//                        File.Copy(Source, Target, true);
//                        local_result = true;
//                        subBlock.Label.InnerHtmlBlock.Add(HTMLSpanOK());
//                    }
//                    catch (Exception e) { subBlock.Label.InnerHtmlBlock.Add(HTMLSpanFAULT()); subBlock.Div.Add(_BRLF + e.Message); }
//                    finally { block.Div.Add(subBlock.ToString()); }
//                    if (local_result)
//                    {
//                        cancellationToken.ThrowIfCancellationRequested();
//                        local_result = false;
//                        var WMIBlock = new PCHTMLBlock(Host + ".WMI");
//                        WMIBlock.Label.InnerHtml("Execute remote script ");
//                        try
//                        {
//                            ProcessWMI WMIProcess = new ProcessWMI();
//                            int timer = Convert.ToInt32(THIS.tTimeout.Text) * 1000;
//                            String TMPFolder = @"c:\windows\temp\";

//                            WMIProcess.ExecuteRemoteProcessWMI(Host,
//                                @"c:\Windows\System32\cmd.exe /c " + TMPFolder + THIS.tScriptFile.Text + " " + DateTimeStamp + " " + THIS.tSourceCopy.Text + " " + THIS.tTargetCopy.Text + " >" + TMPFolder + THIS.tScriptFile.Text + "." + DateTimeStamp + ".log 2>&1"
//                                , timer);


//                            local_result = true;
//                            WMIBlock.Label.InnerHtmlBlock.Add(HTMLSpanOK());
//                        }
//                        catch (Exception e) { WMIBlock.Label.InnerHtmlBlock.Add(HTMLSpanFAULT()); WMIBlock.Div.Add(e.Message+_BRLF); }
//                        finally
//                        {
//                            if (File.Exists(Target))
//                                try
//                                { File.Delete(Target); }
//                                catch { WMIBlock.Div.Add(HTMLTagSpan("Failed to delete script " + Target + _BRLF, CSS_WARM)); }
//                            if (File.Exists(LogFile) )
//                            {
//                                try {
//                                    WMIBlock.Div.Add("<pre>"+File.ReadAllText(LogFile) +"</pre>"+ _BRLF);
//                                    File.Delete(LogFile);
//                                }
//                                catch { WMIBlock.Div.Add(HTMLTagSpan("Failed to delete log " + LogFile + _BRLF, CSS_WARM)); }
//        }
//                            else
//                            { WMIBlock.Div.Add(HTMLTagSpan("No log file" + _BRLF, CSS_WARM)); }

//                            block.Div.Add(WMIBlock.ToString());                        
//                        }
//                        if (local_result)
//                        {
//                            /*block.Div.Add(_BRLF+"Copy ");
//                            const string WMINamespace = @"root\cimv2";
//                            ConnectionOptions WMIScopeOptions = new ConnectionOptions();
//                            WMIScopeOptions.Impersonation = System.Management.ImpersonationLevel.Impersonate;

//                            #region
//                            ManagementScope WMIScope = new ManagementScope(@"\\"+Host+@"\"+WMINamespace, WMIScopeOptions);
//                            //TODO
//                            //try
//                            //{
//                                WMIScope.Connect();
//                            //}
//                            //catch (Exception e)
//                            //{
//                            //    throw new Exception("Management Connect to remote machine " + remoteComputerName + " as user " + strUserName + " failed with the following error " + e.Message);
//                            //}


//                            ObjectQuery WMIquery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
//                            ManagementObjectSearcher WMIsearcher = new ManagementObjectSearcher(WMIScope, WMIquery);
//                            ManagementObjectCollection WMIqueryCollection = WMIsearcher.Get();
//                            foreach (ManagementObject m in WMIqueryCollection)
//                            {                        
//                                block.Div.Add("Computer Name  : " + m["csname"]+_BRLF);                       
//                            }
//                            #endregion
//                            */
//                            result = true;
//                        }
//                    }                    
//                }
//            }
//            catch (OperationCanceledException e) 
//            {
//#if DEBUG
//                Debug.WriteLine("Thread OperationCanceledException " + Host + " RunningThreadCount=" + RunningThreadCount.ToString());
//#endif
//                block.Div.Add(_BRLF + HTMLSpanCANCEL()+" "+e.Message+_BRLF);                //Output.Append("<br>\nCanceled<br>\n" + e.Message + "<br>\n");
//            }
//            catch (Exception e)
//            {
//#if DEBUG
//                Debug.WriteLine("Thread Exception " + Host + " RunningThreadCount=" + RunningThreadCount.ToString());
//#endif 
//                block.Div.Add(e.Message);
//            }
//            finally
//            {
//                 RunningThreadCount--; 
//                if (result)
//                    block.Label.InnerHtmlBlock.Add(HTMLSpanOK());
//                else
//                    if (cancellationToken.IsCancellationRequested)
//                    block.Label.InnerHtmlBlock.Add(HTMLSpanCANCEL());
//                else
//                    block.Label.InnerHtmlBlock.Add(HTMLSpanFAULT());


//                WriteLog(block.ToString());
//#if DEBUG
//                Debug.WriteLine("Thread " + Host + " finally: " + " RunningThreadCount=" + RunningThreadCount.ToString());
//#endif
//            }
//        }
    }
}



/*

Running Command Line in Remote Machine Using WMI https://www.abygeorgea.com/blog/2018/09/08/running-command-line-in-remote-machine-using-wmi/


WMIC /node:192.168.1.100 process call create “cmd.exe /c dir”
C:\Windows\System32\wbem


Here is a little script I made for generic execution of remote commands:
https://4sysops.com/archives/three-ways-to-run-remote-windows-commands/#method-3-use-controlup-to-run-remote-commands




xcopy  /?
Копирует файлы и деревья каталогов.

XCOPY источник [назначение] [/A | /M] [/D[:дата]] [/P] [/S [/E]] [/V] [/W]
                           [/C] [/I] [/Q] [/F] [/L] [/G] [/H] [/R] [/T] [/U]
                           [/K] [/N] [/O] [/X] [/Y] [/-Y] [/Z] [/B] [/J]
                           [/EXCLUDE:файл1[+файл2][+файл3]...]

  источник     Копируемые файлы.
  назначение   Расположение или имена новых файлов.
  /A           Копирует только файлы с установленным атрибутом архивации;
               сам атрибут при этом не изменяется.
  /M           Копирует только файлы с установленным атрибутом архивации;
               после копирования атрибут снимается.
  /D:м-д-г     Копирует файлы, измененные не ранее указанной даты.
               Если дата не указана, заменяются только конечные файлы
               с более ранней датой, чем у исходных файлов.
  /EXCLUDE:файл1[+файл2][+файл3]...
               Список файлов, содержащих строки.  Каждая строка
               должна располагаться в отдельной строке в файлах. Если какая-либо
               из строк совпадает с любой частью абсолютного пути к копируемому
               файлу, такой файл исключается из операции копирования.  Например,
               при указании строки \obj\ или .obj будут исключены
               все файлы из каталога obj или все файлы с расширением
               OBJ соответственно.
  /P           Выводит запросы перед созданием каждого конечного файла.
  /S           Копирует только непустые каталоги с подкаталогами.
  /E           Копирует каталоги с подкаталогами, включая пустые.
               Эквивалент сочетания параметров /S /E. Совместим с параметром /T.
  /V           Проверяет размер каждого нового файла.
  /W           Выводит запрос на нажатие клавиши перед копированием.
  /C           Продолжает копирование вне зависимости от наличия ошибок.
  /I           Если назначение не существует и копируется несколько файлов,
               считается, что местом назначения является каталог.
  /Q           Запрещает вывод имен копируемых файлов.
  /F           Выводит полные имена исходных и конечных файлов во время копирования.
  /L           Выводит копируемые файлы.
  /G           Копирует зашифрованные файлы в конечную папку,
               не поддерживающую шифрование.
  /H           Копирует скрытые и системные файлы (среди прочих).
  /R           Разрешает замену файлов, предназначенных только для чтения.
  /T           Создает структуру каталогов (кроме пустых каталогов)
               без копирования файлов. Для создания пустых каталогов и подкаталогов
               используйте сочетание параметров /T /E.
  /U           Копирует только файлы, уже имеющиеся в конечной папке.
  /K           Копирует атрибуты. При использовании команды XСOPY обычно
               сбрасываются атрибуты "только для чтения".
  /N           Использует короткие имена при копировании.
  /O           Копирует сведения о владельце и данные ACL.
  /X           Копирует параметры аудита файлов (требуется параметр /O).
  /Y           Подавляет запрос на подтверждение перезаписи
               существующего конечного файла.
  /-Y          Обязательный запрос на подтверждение перезаписи
               существующего конечного файла.
  /Z           Копирует сетевые файлы с возобновлением.
  /B           Копирует символьную ссылку вместо ее целевого объекта.
  /J           Копирует с использованием ввода-вывода без буферизации.
               Рекомендуется для очень больших файлов.

Параметр /Y можно установить заранее через переменную среды COPYCMD.
Параметр /-Y командной строки переопределяет такую установку.

copy /?
Копирование одного или нескольких файлов в другое место.

COPY [/D] [/V] [/N] [/Y | /-Y] [/Z] [/L] [/A | /B] источник [/A | /B]
     [+ источник [/A | /B] [+ ...]] [результат [/A | /B]]

  источник     Имена одного или нескольких копируемых файлов.
  /A           Файл является текстовым файлом ASCII.
  /B           Файл является двоичным файлом.
  /D           Указывает на возможность создания зашифрованного файла
  результат    Каталог и/или имя для конечных файлов.
  /V           Проверка правильности копирования файлов.
  /N           Использование, если возможно, коротких имен при копировании
               файлов, чьи имена не удовлетворяют стандарту 8.3.
  /Y           Подавление запроса подтверждения на перезапись существующего
               конечного файла.
  /-Y          Обязательный запрос подтверждения на перезапись существующего
               конечного файла.
  /Z           Копирование сетевых файлов с возобновлением.
  /L           Если источник является символической ссылкой, копирование
               ссылки вместо реального файла, на который указывает ссылка.

Ключ /Y можно установить через переменную среды COPYCMD.
Ключ /-Y командной строки переопределяет такую установку.
По умолчанию требуется подтверждение, если только команда COPY
не выполняется в пакетном файле.

Чтобы объединить файлы, укажите один конечный и несколько исходных файлов,
используя подстановочные знаки или формат "файл1+файл2+файл3+...".

 */
