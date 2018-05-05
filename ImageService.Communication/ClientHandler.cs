using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    public class ClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    string commandLine = reader.ReadString();
                    Console.WriteLine("In client handler recieved " + commandLine);
                    //string result = ExecuteCommand(commandLine, client);
                    string result = "My answer is " + commandLine;
                    Console.WriteLine("In client handler sending {0}", result);
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }
    }
}
