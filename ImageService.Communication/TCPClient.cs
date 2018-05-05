﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    public class TCPClient : IClient    
    {
        private string ip;
        private int port;
        private TcpClient client;
        public TCPClient(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            this.client = new TcpClient();
        }
        public void Connect()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.ip), this.port);
            client.Connect(ep);
        }
        public void Send(string data)
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // Send data to server
                Console.WriteLine("In Client send: {0}", data);
                writer.Write(data);
            }
        }
        public string Recieve()
        {
            this.Connect();
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // Get result from server
                string data = reader.ReadString();
                Console.WriteLine("In Client recieved: {0}", data);
                return data;
            }
        }
        public string sendrecieve(string data)
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // Send data to server
                Console.WriteLine("In Client send: {0}", data);
                writer.Write(data);
                // Get result from server
                data = reader.ReadString();
                Console.WriteLine("In Client recieved: {0}", data);
                return data;
            }
        }
        public void Disconnect()
        {
            Console.WriteLine("client is disconnected...");
            client.Close();
        }
    }
}
