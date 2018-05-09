using Newtonsoft.Json;
using System;
using ImageService.Commands;
using ImageService.Communication;
using ImageService.Infrastructure;
using Newtonsoft.Json.Linq;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            #region test client
            /*
            string ip = "127.0.0.1";
            int port = 8000;
            IClient c1 = new TCPClient(ip, port);
            c1.Connect();
            Console.WriteLine("c1 is connected to server");


            Console.WriteLine("Enter command id:");
            int commandID = int.Parse(Console.ReadLine());
            c1.sendrecieve(commandID.ToString());
            c1.Disconnect();
            Console.ReadLine();
            */
            #endregion
            #region 2nd client
            /*
            IClient c2 = new TCPClient(ip, port);
            c2.Connect();
            Console.WriteLine("c2 is connected to server");
            
            Console.WriteLine(c1.sendrecieve("this is c1"));
            Console.WriteLine(c2.sendrecieve("this is c2"));
            */
            #endregion

            
            GetConfigCommand command = new GetConfigCommand();
            string str = JsonConvert.SerializeObject(command, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            Console.WriteLine(str);
            ICommand cmd = JsonConvert.DeserializeObject<ICommand>(str, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            Console.WriteLine(cmd.ToString());

            //int commandID = (int)CommandEnum.NewFileCommand;
            //Console.WriteLine(commandID);
            //CommandEnum commandname = CommandEnum.NewFileCommand;
            //Console.WriteLine(commandname);
            Console.ReadLine();
        }
    }
}
