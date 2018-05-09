using ImageService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using ImageService.Infrastructure;

namespace ImageService.Commands
{
    public class GetConfigCommand : ICommand
    {
        public GetConfigCommand()
        {
        }
        public string Execute(string[] args, out bool result)
        {
            result = true;
            string handler = "handler=" + AppConfigReader.Instance.GetValueByKey("Handler");
            string outputDir = "outputDir=" + AppConfigReader.Instance.GetValueByKey("OutputDir");
            string sourceName = "sourceName=" + AppConfigReader.Instance.GetValueByKey("SourceName");
            string logName = "logName=" + AppConfigReader.Instance.GetValueByKey("LogName");
            string thumbnailSize = "thumbnailSize=" + AppConfigReader.Instance.GetValueByKey("ThumbnailSize");

            string[] data = { handler , outputDir , sourceName , logName , thumbnailSize};
            return String.Join(Environment.NewLine, data);
        }
    }
}
