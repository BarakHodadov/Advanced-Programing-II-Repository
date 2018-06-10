using ImageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class LogsModel
    {
        private WebClient client;

        public List<Log> logsList { get; set; }

        public LogsModel()
        {
            this.client = WebClient.Instance;
            logsList = new List<Log>();
            string[] logs = client.sendrecieve(client.makeData(ImageService.Infrastructure.CommandEnum.LogCommand)).Split(';');
            foreach (string l in logs)
            {
                if(!l.Equals(""))
                {
                    string[] data = l.Split('#');
                    logsList.Add(new Log(data[0], data[1]));
                }
            }
        }
    }
}