using ImageService.Communication;
using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class WebClient
    {
        private static WebClient instance = null;
        private TCPClient client;
        private static readonly object my_lock = new object();
        private WebClient()
        {
            this.client = new TCPClient("127.0.0.1", 8000);
        }

        public void Connect()
        {
            if (!this.client.isConnected())
                client.Connect();
        }
        public void recieve()
        {
            this.client.Recieve();
        }

        public string sendrecieve(string data)
        {
            this.Connect();
            return this.client.sendrecieve(data);
        }

        public static WebClient Instance
        {
            get
            {
                lock (my_lock)
                {
                    if (instance == null)
                    {
                        instance = new WebClient();
                    }
                    return instance;
                }
            }
        }

        public string makeData(CommandEnum cmd, string[] args = null)
        {
            string data = CommandConvertor.Instance.ConvertCommandToID(cmd) + ";";
            if (args != null)
            {
                data = data + String.Join(";", args);
            }
            return data;
        }
    }
}