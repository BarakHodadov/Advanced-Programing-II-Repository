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
            string handler = AppConfigReader.Instance.GetValueByKey("Handler");
            string outputDir = AppConfigReader.Instance.GetValueByKey("OutputDir");
            string sourceName = AppConfigReader.Instance.GetValueByKey("SourceName");
            string logName = AppConfigReader.Instance.GetValueByKey("LogName");
            string thumbnailSize = AppConfigReader.Instance.GetValueByKey("ThumbnailSize");

            string[] data = { handler , outputDir , sourceName , logName , thumbnailSize};
            return String.Join("#", data);
        }
    }
}
