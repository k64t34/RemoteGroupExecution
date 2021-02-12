using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Web;


namespace RGE
{
    public partial class Form1
    {
        async void Run_Copy()
        {
            await Task.Run(() =>
            {
            StringBuilder Output = new StringBuilder();            
            Output.Append(HTMLTagSpan(" Copy from:", CSS_BaseHighLight) + tSourceCopy.Text );                
            if (!Directory.Exists(tSourceCopy.Text))
            {
                Output.Append(HTMLTagSpan(" Folder doesn't exist", CSS_FAULT)+ _BRLF);                
                WriteLog(Output);
                goto Run_Copy_End_Run;                //return;
            }
            Output.Append(HTMLSpanOK() + _BRLF);
            Output.Append(HTMLTagSpan("Copy to  :", CSS_BaseHighLight) + tTargetCopy.Text + _BRLF);
            Output.Append(HTMLTagSpan("Override:", CSS_BaseHighLight) + chkCopyOverride.Checked + _BRLF);                
            if (chkCopyOverride.Checked)Output.Append(HTMLTagSpan("Only newer:", CSS_BaseHighLight) + chkCopyOnlyNewer.Checked + _BRLF);
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
             WriteLog(_BRLF);                
#if DEBUG
            Debug.WriteLine("All Thread finished");
#endif
                Run_Copy_End_Run:End_Run();                
            });
        }
        //************************************************************
        static void Do_Copy(int Index, CancellationToken cancellationToken)                    {
        //************************************************************
            RunningThreadCount++;
            bool result = false;
            string Host = THIS.chkList_PC.Items[(int)Index].ToString();
#if DEBUG
            Debug.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " start: " + Host/*+ " FreeThreadCount="+ FreeThreadCount.ToString()*/);
#endif
            var block = new PCHTMLBlock(Host);            
            try
            {              
                cancellationToken.ThrowIfCancellationRequested();
                block.Div.Add("Ping ");
                if (!Ping(Host))
                    block.Div.Add(HTMLSpanFAULT());
                else
                {
                    block.Div.Add(HTMLSpanOK());
                    cancellationToken.ThrowIfCancellationRequested();
                    //Output.Append("<br>\n");
                    result = true;
                }
            }
            catch (OperationCanceledException e) 
            {
#if DEBUG
                Debug.WriteLine("Thread OperationCanceledException " + Host + " RunningThreadCount=" + RunningThreadCount.ToString());
#endif
                block.Div.Add(_BRLF + HTMLSpanCANCEL()+_BRLF);                //Output.Append("<br>\nCanceled<br>\n" + e.Message + "<br>\n");
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine("Thread Exception " + Host + " RunningThreadCount=" + RunningThreadCount.ToString());
#endif 
                //Output.Append("<br>\nException<br>\n" + e.Message + "<br>\n");
            }
            finally
            {                
                RunningThreadCount--;
                if (result)
                    block.Label.InnerHtmlBlock.Add(HTMLSpanOK());
                else
                    if (cancellationToken.IsCancellationRequested)
                    block.Label.InnerHtmlBlock.Add(HTMLSpanCANCEL());
                else
                    block.Label.InnerHtmlBlock.Add(HTMLSpanFAULT());


                WriteLog(block.ToString());
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