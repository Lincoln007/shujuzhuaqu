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
                ConfigurationManager.AppSettings["workingpath"] = value;
            }
        }
    }
}
