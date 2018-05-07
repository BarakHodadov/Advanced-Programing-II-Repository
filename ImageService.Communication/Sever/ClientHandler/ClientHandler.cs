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
        public event execute executeCommand;
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
                    string result = ExecuteCommand(commandLine, client);
                    Console.WriteLine("In client handler sending " + result);
                    writer.Write(result);
                }
                client.Close();
            }).Start();
        }

        public string ExecuteCommand(string commandLine, TcpClient client)
        {
           return this.executeCommand?.Invoke(Int32.Parse(commandLine[0].ToString()), commandLine.Substring(1).Split(';'), out bool resultSuccesful);
        }
    }
}
