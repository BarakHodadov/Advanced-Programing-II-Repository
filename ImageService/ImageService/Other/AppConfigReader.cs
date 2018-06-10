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
        public string GetValueByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        public void ChangeValueByKey(string key, string value)
        {
            // Got this from here: https://social.msdn.microsoft.com/Forums/vstudio/en-US/b865ce7a-6616-4109-90a5-553efc928075/modify-connectionstring-in-appconfig?forum=csharpgeneral
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSetting");
        }
        
        // Indexer.
        public string this[string key]
        {
            get
            {
                return ConfigurationManager.AppSettings[key];
            }
            set
            {
                // Got this from here: https://stackoverflow.com/questions/5468342/how-to-modify-my-app-exe-config-keys-at-runtime?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings[key].Value = value;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }
        
         /// <summary>
         /// Remove handler method.
         /// Change the app config by removing the handler.
         /// </summary>
         /// <param name="handler"></param>
        public void removeHandler(string handlerToRemove)
        {
            string handlers = this["Handler"];
            Console.WriteLine("my current handlers are " + handlers);
            string[] handlersList = handlers.Split(';');
            List<string> newHandlers = new List<string>();
            foreach (string handler in handlersList)
            {
                if (handler != handlerToRemove)
                {
                    newHandlers.Add(handler);
                }
            }
            handlers = String.Join(";", newHandlers);
            Console.WriteLine("after " + handlers);
            this["Handler"] = handlers;
            //handlers = String.Join(";", handlersList);



            //int index = handlers.IndexOf(handler);
            //handlers = handlers.Remove(index, handler.Length + 1);

            //this["Handler"] = handlers;
            Console.WriteLine("changed handlers to:" + newHandlers);
        }
    }
}
