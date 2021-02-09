using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace RGE
{
    public partial class Form1
    {
        async void Run_Copy()
        {
            await Task.Run(() =>
            {
            StringBuilder Output = new StringBuilder();
            Output.Append(t_color("yellow", "Host:") + Environment.MachineName + _BR + "\n");
            Output.Append(t_color("yellow", "Copy from:") + tSourceCopy.Text);
            if (!Directory.Exists(tSourceCopy.Text))
            {
                Output.Append(t_color("red", " Folder doesn't exist"));
                Output.Append(_BR + "\n");
                WriteLog(Output);
                return;
            }
            Output.Append(_BR + "\n");
            Output.Append(t_color("yellow", "Copy to  :") + tTargetCopy.Text + _BR + "\n");
            Output.Append(t_color("yellow", "Override:") + chkCopyOverride.Checked + _BR + "\n");
            if (chkCopyOverride.Checked)
                Output.Append(t_color("yellow", "Only newer:") + chkCopyOnlyNewer.Checked + _BR + "\n");            
            
            Output.Append(t_color("yellow", "Hosts:")+ _BR + "\n");
            RunningThreadCount = 0;
            foreach (int indexChecked in chkList_PC.CheckedIndices)
            {
                Output.Append(String.Format("{0}.{1}<br>\n", ++RunningThreadCount, chkList_PC.Items[indexChecked].ToString()));
            }
            Output.Append("<br>\n");
            WriteLog(Output);
            
            RunningThreadCount = 0;
            Task task;
            foreach (int indexChecked in chkList_PC.CheckedIndices)
            {
                if (MainStatus == 0) { break; }
                task = new Task(() => Do_Copy(indexChecked, cts.Token)) ;
                task.Start();
                Thread.Sleep(1);
                }                                
            while (RunningThreadCount!=0)
            {
#if DEBUG
                 Debug.WriteLine("Wait Thread finish: " + RunningThreadCount.ToString());
#endif
                 Thread.Sleep(1000);                     
            }
             WriteLog("<br>\n");                
#if DEBUG
            Debug.WriteLine("All Thread finished");
#endif
                End_Run();                
            });
        }
        //************************************************************
        static void Do_Copy(int Index, CancellationToken cancellationToken)                    {
        //************************************************************
            RunningThreadCount++;
            string Host = THIS.chkList_PC.Items[(int)Index].ToString();
#if DEBUG
            Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " start: " + Host/*+ " FreeThreadCount="+ FreeThreadCount.ToString()*/);
#endif
            StringBuilder Output = new StringBuilder();
            Output.Append(@"<detaildsh>");
            try
            {               
                Output.Append(@"<summary>" + t_color("white", Host)+ @"</summary>");
                cancellationToken.ThrowIfCancellationRequested();
                if (!Ping(Host)) Output.Append(" " + t_color("red", " no ping") + " " /*+ __Error*/);
                cancellationToken.ThrowIfCancellationRequested();                                
                Output.Append("<br>\n");
            }
            catch (OperationCanceledException e) 
            {
#if DEBUG
                Debug.WriteLine("Thread OperationCanceledException " + Host + " RunningThreadCount=" + RunningThreadCount.ToString());
#endif
                Output.Append("<br>\nCanceled<br>\n" + e.Message + "<br>\n");
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine("Thread Exception " + Host + " RunningThreadCount=" + RunningThreadCount.ToString());
#endif 
                Output.Append("<br>\nException<br>\n" + e.Message + "<br>\n");
            }
            finally
            {
                
                RunningThreadCount--;
                Output.Append(@"</details>");
                WriteLog(Output);
#if DEBUG
                Debug.WriteLine("Thread " + Host + " finally: " + " RunningThreadCount=" + RunningThreadCount.ToString());
#endif
            }
        }
    }
}
    


/*
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