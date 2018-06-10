using ImageService.Controller.Handlers;
using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class RemoveHandlerModel
    {
        WebClient client;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handler Path")]
        public string HandlerPath { get; set; }

        public RemoveHandlerModel(string _handler)
        {
            this.client = WebClient.Instance;
            this.HandlerPath = _handler;
        }

        public void removeHandler()
        {
            client.sendrecieve(client.makeData(CommandEnum.CloseCommand, new string[] { this.HandlerPath } ));
        }
    }
}