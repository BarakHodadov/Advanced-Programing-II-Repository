﻿using System;
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
        /// <summary>
        /// Client handler method. Handle client requests.
        /// </summary>
        /// <param name="client"></param>
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                NetworkStream stream = client.GetStream();
                BinaryWriter writer = new BinaryWriter(stream);
                BinaryReader reader = new BinaryReader(stream);

                //using (NetworkStream stream = client.GetStream())
                //using (BinaryReader reader = new BinaryReader(stream))
                //using (BinaryWriter writer = new BinaryWriter(stream))
                
                // Handle a client's specific request.
                new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            string commandLine = reader.ReadString();
                            Console.WriteLine("In client handler recieved " + commandLine);
                            string result = ExecuteCommand(commandLine, client);
                            Console.WriteLine("In client handler sending " + result);
                            writer.Write(result);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                }).Start();
                    //NetworkStream stream = client.GetStream();
                    //BinaryWriter writer = new BinaryWriter(stream);
                    //BinaryReader reader = new BinaryReader(stream);

                    //string commandLine = reader.ReadString();
                    //Console.WriteLine("In client handler recieved " + commandLine);
                    //string result = ExecuteCommand(commandLine, client);
                    //Console.WriteLine("In client handler sending " + result);
                    //writer.Write(result);
                //}
                //client.Close();
            }).Start();

            //client.Close();
        }

        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            return this.executeCommand?.Invoke(Int32.Parse(commandLine[0].ToString()), commandLine.Substring(2).Split(';'), out bool resultSuccesful);
            //ICommand cmd = JsonConvertor.Instance.FromJson(commandLine);
            //return cmd.Execute(commandLine.Split('D'), out bool result);
        }
    }
}
