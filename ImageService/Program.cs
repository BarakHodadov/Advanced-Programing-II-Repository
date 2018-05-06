using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ImageService;

namespace ImageService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {   
            string sourceName = AppConfigReader.Instance.GetValueByKey("SourceName");
            string logName = AppConfigReader.Instance.GetValueByKey("LogName");

            string[] values = { sourceName, logName };
            ImageService service = new ImageService(values);

            if (Environment.UserInteractive)
            {
                service.RunAsConsole(values);
            }

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                service
            };
            ServiceBase.Run(ServicesToRun);

        }        
    }
}
