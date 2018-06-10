using ImageService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ImageService.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using ImageService.Controller.Handlers;

namespace WebApplication2.Models
{
    public class ConfigModel
    {
        private WebClient client;

        public IEnumerable<string> handlersList { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Source Name")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Output Dir")]
        public string OutputDir { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Log Name")]
        public string LogName { get; set; }

        [Required]
        [Display(Name = "Thumbnail Size")]
        public string ThumbnailSize { get; set; }

        public ConfigModel()
        {
            this.client = WebClient.Instance;
            string[] config = this.client.sendrecieve(this.client.makeData(CommandEnum.GetConfigCommand)).Split('#');

            this.OutputDir = config[1];
            this.SourceName = config[2];
            this.LogName = config[3];
            this.ThumbnailSize = config[4];

            if (config[0].Equals(""))
                handlersList = new List<string>();
            else
                handlersList = new List<string>(config[0].Split(';'));
        }
    }
}