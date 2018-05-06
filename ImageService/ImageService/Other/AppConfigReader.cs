using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ImageService
{
    // Copy implementation from here: https://msdn.microsoft.com/en-us/library/ff650316.aspx
    public sealed class AppConfigReader
    {
        private static AppConfigReader instance = null;
        private static readonly object my_lock = new object();

        private AppConfigReader()
        {
        }

        public static AppConfigReader Instance
        {
            get
            {
                lock (my_lock)
                {
                    if (instance == null)
                    {
                        instance = new AppConfigReader();
                    }
                    return instance;
                }
            }
        }
        public string GetValueByKey(string value)
        {
            return ConfigurationManager.AppSettings[value];
        }
    }
}
