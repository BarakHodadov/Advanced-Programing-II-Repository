using System;
using ImageService.Communication;

namespace TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = new TCPServer("127.0.0.1", 8000, new ClientHandler());
            server.Start();
        }
    }
}
