using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportData
{
    public class AppConfigContext
    {
        private AppConfigContext()
        {
        }
        private static AppConfigContext instance = new AppConfigContext();

        public static AppConfigContext Instance { get => instance; }
        public string WorkingPath
        {
            get
            {
               string path= ConfigurationManager.AppSettings["workingpath"];
                if(string.IsNullOrEmpty(path))
                {
                    return System.Windows.Forms.Application.StartupPath;
                }
                else
                {
                    return path;
                }
            }
            set
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings["workingpath"] == null)
                {
                    settings.Add("workingpath", value);
                }
                else
                {
                    settings["workingpath"].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

                //ConfigurationManager.AppSettings["workingpath"] = value;
            }
        }
    }
}
