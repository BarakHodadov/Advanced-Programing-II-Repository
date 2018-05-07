using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    public class TCPServer : IServer
    {
        private string ip;
        private int port;
        private TcpListener listener;
        private IClientHandler ch;

        public event execute OnCommandRecieved;

        public TCPServer(string ip, int port, IClientHandler ch)
        {
            this.ip = ip;
            this.port = port;
            this.ch = ch;
            ch.executeCommand += Temp;
            ch.executeCommand += this.OnCommandRecieved;
        }
        public void Start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.ip), this.port);
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");

            //Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            //});
            //task.Start();
        }
        public void Stop()
        {
            listener.Stop();
        }

        public string Temp(int id,string[] args, out bool resultSuccesful)
        {
            resultSuccesful = true;
            return this.OnCommandRecieved?.Invoke(id,args,out resultSuccesful);
        }
    }
}
