using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    class test
    {
        public static void Main()
        {
            Console.WriteLine("Start");
            string ip = "127.0.0.1";
            int port = 8000;

            IServer server = new TCPServer(ip, port, new ClientHandler());
            server.Start();

            
        }
    }
}
