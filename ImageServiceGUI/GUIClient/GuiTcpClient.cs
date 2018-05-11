using ImageService.Communication;
using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.GUIClient
{
    public sealed class GUITCPClient
    {
        private static GUITCPClient instance = null;
        private TCPClient client;
        private static readonly object my_lock = new object();
        private GUITCPClient()
        {
            this.client = new TCPClient("127.0.0.1", 8000);
        }

        public void Connect()
        {
            if (!this.client.isConnected())
                client.Connect();
        }

        public string sendrecieve(string data)
        {
            this.Connect();
            return this.client.sendrecieve(data);
        }

        public static GUITCPClient Instance
        {
            get
            {
                lock (my_lock)
                {
                    if (instance == null)
                    {
                        instance = new GUITCPClient();
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
