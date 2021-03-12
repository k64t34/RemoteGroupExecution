//consume values from a custom config section https://code.4noobz.net/csharp-store-variables-and-collection-in-config-file/
//How to: Create Custom Configuration Sections Using ConfigurationSection https://docs.microsoft.com/en-us/previous-versions/2tw134k3(v=vs.140)?redirectedfrom=MSDN
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Windows.Forms;
using KeyValue = System.Collections.Generic.KeyValuePair<string, string>;//https://stackoverflow.com/questions/52947538/get-multiple-instances-of-the-same-key-within-custom-section
using System.Collections;
using System.IO;
using System.Threading;
using static System.Windows.Forms.CheckedListBox;

namespace RGE
{
    class Config
    {
        public const String FileHostsConfig = "hosts.txt";
        #region HostsSection
        /*public class HostsSection : ConfigurationSection //https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationelementcollection?view=dotnet-plat-ext-5.0
        {
            // Declare the UrlsCollection collection property.
            [ConfigurationProperty("Hosts", IsDefaultCollection = false)]
            [ConfigurationCollection(typeof(UrlsCollection),
                AddItemName = "add",
                ClearItemsName = "clear",
                RemoveItemName = "remove")]
        }*/
        #endregion
        #region CustomSection
        /*
        public sealed class mycustom : ConfigurationSection //https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationproperty?view=dotnet-plat-ext-5.0
        {
            private static ConfigurationPropertyCollection _Properties;

            // The FileName property.
            private static ConfigurationProperty _Host;

            // The Alias property.
            private static ConfigurationProperty _Checked;
            static mycustom()// CustomSection constructor.
            {
                // Initialize the _FileName property
                _Host =
                    new ConfigurationProperty("hostName",
                    typeof(string), "default.txt");

                // Initialize the _MaxUsers property
                _Checked =
                    new ConfigurationProperty("checked",
                    typeof(bool), false,
                    ConfigurationPropertyOptions.None);
                // Initialize the Property collection.
                _Properties = new ConfigurationPropertyCollection();
                _Properties.Add(_Host);
                _Properties.Add(_Checked);
            }
            protected override ConfigurationPropertyCollection Properties
            {
                get
                {
                    return _Properties;
                }
            }            
            public void ClearCollection()// Clear the property.
            {
                Properties.Clear();
            }
            public void RemoveCollectionElement(string elName)// Remove an element from the property collection.
            {
                Properties.Remove(elName);
            }            
            public IEnumerator GetCollectionEnumerator() // Get the property collection enumerator.
            {
                return (Properties.GetEnumerator());
            }
            [StringValidator(InvalidCharacters = " ~!@#$%^&*()[]{}/;'\"|\\",
            MinLength = 1, MaxLength = 60)]
            public string FileName
            {
                get
                {
                    return (string)this["fileName"];
                }
                set
                {
                    this["fileName"] = value;
                }
            }
        }
            public sealed class KeyValueHandler : IConfigurationSectionHandler
        {
            public object Create(object parent, object configContext, XmlNode section)
            {
                var result = new List<KeyValue>();
                foreach (XmlNode child in section.ChildNodes)
                {
                    var key = child.Attributes["key"].Value;
                    var value = child.Attributes["value"].Value;
                    result.Add(new KeyValue(key, value));
                }
                return result;
            }
        }*/
        #endregion

        public static void WriteSettingsHosts(CheckedListBox chkList, String cfgFile)
        {
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(cfgFile, false, Encoding.UTF8);
                for (int i = 0; i != chkList.Items.Count; i++) { sw.WriteLine(chkList.Items[i].ToString() + ";" + chkList.GetItemChecked(i).ToString()); }
                sw.Close();
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine("Error writing list of hosts " + e.Message);
#endif
            }
        }
        public static void WriteSettingsHosts(CheckedListBox chkList)            {            WriteSettingsHosts(chkList,Path.Combine(Application.StartupPath + @"\" + Environment.UserName + "." + FileHostsConfig));        }
        public static void ReadSettingsHosts(CheckedListBox chkList,String cfgFile)
        {
            try
            {
                if (File.Exists(cfgFile))
                {
                    chkList.Items.Clear();
                    int i;
                    using (StreamReader sr = new StreamReader(cfgFile))
                    {
                        while (sr.Peek() >= 0)
                        {
                            String l = sr.ReadLine();
                            string[] p;
                            if (l.IndexOf(';') != -1) p = /*new string[2];p=*/l.Split(new Char[] { ';' }, 2);
                            else if (l.IndexOf(',') != -1) p = /*new string[2];p=*/l.Split(new Char[] { ',' }, 2);
                            else p = new string[] { l, "True" };
                            i = chkList.Items.Add(p[0]);
                            bool b;
                            if (!Boolean.TryParse(p[1], out b)) { b = true; }
                            chkList.SetItemChecked(i, b);
                        }
                    }
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine("Error writing list of hosts " + e.Message);
#endif
            }
        }
        public static void ReadSettingsHosts(CheckedListBox chkList)        {            ReadSettingsHosts(chkList, Path.Combine(Application.StartupPath + @"\" + Environment.UserName + "." + FileHostsConfig));        }

        public static void WriteSettings(string key, string value)
        {
            try
            {
                System.Configuration.ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = Path.GetDirectoryName(Application.ExecutablePath)+"\\"+ System.Environment.UserName+".xml" ;
                System.Configuration.Configuration configFile = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);                //var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel./*PerUserRoamingAndLocal*/None); 
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
#if DEBUG
                Debug.WriteLine("Error writing app settings "+key+" "+value );
#endif
            }
        }
        public static void WriteSettings(string key, IList<KeyValue> list) // TODO ////https://stackoverflow.com/questions/52947538/get-multiple-instances-of-the-same-key-within-custom-section
        {
            /*try
            {
                var list = (IList<KeyValue>)ConfigurationManager.GetSection("section1");

                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
#if DEBUG
                Debug.WriteLine("Error writing app settings " + key + " " + value);
#endif
            }*/
        }
        
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
        public static void ReadSetting(string key, ref string variable)
        {
            try
            {                
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings[key] == null) throw new Exception();
                variable = appSettings[key];                
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine("Error get app settings " + key + " " + e.Message);
#endif
            }
        }
        public static String ReadSetting(string key)
        {
            String Result = String.Empty;
            try
            {
                System.Configuration.ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                fileMap.ExeConfigFilename = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + System.Environment.UserName + ".xml";
                System.Configuration.Configuration configFile = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);                //var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel./*PerUserRoamingAndLocal*/None); 
                var appSettings = configFile.AppSettings.Settings;                
                if (appSettings[key] == null) throw new Exception();
                Result = appSettings[key].Value;
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine("Error get app settings " + key + " " + e.Message);
#endif
            }
            return Result;
        }
    }
}