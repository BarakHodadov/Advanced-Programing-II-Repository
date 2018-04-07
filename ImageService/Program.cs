using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ImageService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            string logName = ConfigurationManager.AppSettings["LogName"];
            string sourceName = ConfigurationManager.AppSettings["SourceName"];
            string outputDir = ConfigurationManager.AppSettings["OutputDir"];
            string handlerDir = ConfigurationManager.AppSettings["Handler"];
            int thumbnailSize = int.Parse(ConfigurationManager.AppSettings["ThumbnailSize"]);

            string[] args = { logName, sourceName };

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ImageService(args)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
