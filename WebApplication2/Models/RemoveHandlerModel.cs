using ImageService.Controller.Handlers;
using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class RemoveHandlerModel
    {
        WebClient client;
        string handlerPath;

        public RemoveHandlerModel(string handler)
        {
            this.client = WebClient.Instance;
            this.handlerPath = handler;
        }

        public void removeHandler()
        {
            client.sendrecieve(client.makeData(CommandEnum.CloseCommand, new string[] { this.handlerPath } ));
        }


    }
}