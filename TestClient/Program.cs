﻿using System;
using ImageService.Communication;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 8000;
            IClient c1 = new TCPClient(ip, port);
            c1.Connect();
            Console.WriteLine("c1 is connected to server");

            /*
            IClient c2 = new TCPClient(ip, port);
            c2.Connect();
            Console.WriteLine("c2 is connected to server");
            
            Console.WriteLine(c1.sendrecieve("this is c1"));
            Console.WriteLine(c2.sendrecieve("this is c2"));
            */

            Console.WriteLine("Enter command id:");
            int commandID = int.Parse(Console.ReadLine());
            c1.sendrecieve(commandID.ToString());
            c1.Disconnect();
            Console.ReadLine();
        }
    }
}
