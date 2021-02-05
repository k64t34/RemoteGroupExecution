using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using System.Diagnostics;

namespace RGE
{

    public partial class Form1
    {
        async void Run_Copy()
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
            int i = 0;
            Output.Append(t_color("yellow", "Hosts:")+ _BR + "\n");
            foreach (int indexChecked in chkList_PC.CheckedIndices)
            {
                Output.Append(String.Format("{0}.{1}<br>\n", ++i, chkList_PC.Items[indexChecked].ToString()));
            }
            Output.Append("<br>\n");
            WriteLog(Output);

            await Task.Run(() =>
            {
                i = 0;
                FreeThreadCount = ThreadCount;


                foreach (int indexChecked in chkList_PC.CheckedIndices)
                {
                    while (FreeThreadCount == 0) Thread.Sleep(1000);
                    FreeThreadCount--;
                    Do_Copy(indexChecked);                    
                }
                while (FreeThreadCount != ThreadCount) 
                    {
#if DEBUG
            Debug.WriteLine("Wait Thread finish: " + FreeThreadCount.ToString());
#endif
                    Thread.Sleep(1000); 
                    }

                WriteLog("<br>\n");
                /*
                 https://habr.com/ru/post/165729/
                 ThreadPool Class https://docs.microsoft.com/en-us/dotnet/api/system.threading.threadpool?redirectedfrom=MSDN&view=net-5.0
                 */
#if DEBUG
                Debug.WriteLine("All Thread finished");
#endif
                Stop_Run();
            });
        }
        async void Do_Copy(int Index)
        {
            

            await Task.Run(() =>
            {
#if DEBUG
                Debug.WriteLine("Thread start: " + Index.ToString()+ " FreeThreadCount="+ FreeThreadCount.ToString());
#endif
                StringBuilder Output = new StringBuilder();
                string Host = chkList_PC.Items[Index].ToString();
                Output.Append(t_color("white", Host) );

                if (!Ping(Host)) Output.Append(" "+ t_color("red"," no ping")+" "+ __Error);
                //Работа с потоками в C# http://rsdn.org/article/dotnet/CSThreading1.xml

                /*Public Dim  MAXThreadCount As Integer = 100
                While ThreadCount> MAXThreadCount
                    Threading.Thread.Sleep(1000)
                End While
                Dim myTest As New ThreadsGetMACfromHOST(client.Name)
                Dim bThreadStart As New ThreadStart(AddressOf myTest.GetMACfromHOST)
                Dim bThread As New Thread(bThreadStart)
                bThread.Start*/
                Output.Append("<br>\n");
                WriteLog(Output);
                FreeThreadCount++;
#if DEBUG
                Debug.WriteLine("Thread stop: " + Index.ToString() + " FreeThreadCount=" + FreeThreadCount.ToString());
#endif
            });
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