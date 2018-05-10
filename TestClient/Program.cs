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
        public static string makeData(CommandEnum cmd, string[] args = null)
        {
            string data = CommandConvertor.Instance.ConvertCommandToID(cmd) + ";";
            if (args != null)
            {
                data = data + String.Join(';', args);
            }
            return data;
        }
        static void Main(string[] args)
        {
            #region test client
            string ip = "127.0.0.1";
            int port = 8000;
            IClient c1 = new TCPClient(ip, port);
            c1.Connect();
            Console.WriteLine("c1 is connected to server");


            Console.WriteLine("Enter command id:");
            //int commandID = int.Parse(Console.ReadLine());
            //c1.sendrecieve(commandID.ToString());
            string data = makeData(CommandEnum.CloseCommand, new string[] { @"C:\Users\Barak\Desktop\temp" });
            
            Console.WriteLine("Sent data : " + data);
            c1.sendrecieve(data);
            c1.Disconnect();
            Console.ReadLine();

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

            #region json
            /*
            JsonConvertor convertor = new JsonConvertor();
            GetConfigCommand command = new GetConfigCommand();
            string str = convertor.ToJson(command);
            Console.WriteLine(str);
            ICommand cmd = convertor.FromJson(str);
            Console.WriteLine(cmd.ToString());
            */
            #endregion

            #region command enum
            //int commandID = (int)CommandEnum.NewFileCommand;
            //Console.WriteLine(commandID);
            //CommandEnum commandname = CommandEnum.NewFileCommand;
            //Console.WriteLine(commandname);
            #endregion
            Console.ReadLine();
        }
    }
}