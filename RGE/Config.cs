//consume values from a custom config section https://code.4noobz.net/csharp-store-variables-and-collection-in-config-file/
//How to: Create Custom Configuration Sections Using ConfigurationSection https://docs.microsoft.com/en-us/previous-versions/2tw134k3(v=vs.140)?redirectedfrom=MSDN
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGE
{
    class Config
    {


        static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }
        static void ReadSetting(string key, ref string variable)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(key); Console.Write(" ");
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings[key] == null) throw new Exception();
                variable = appSettings[key];
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(variable);
            }
            catch /*(ConfigurationErrorsException)*/
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(variable);
            }
        }
    }
}