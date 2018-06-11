using Newtonsoft.Json;
using System;
using ImageService.Commands;
using ImageService.Communication;
using ImageService.Infrastructure;
using Newtonsoft.Json.Linq;
using ImageServiceGUI.GUIClient;
using System.IO;

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
            /*
            GUITCPClient client = GUITCPClient.Instance;
            client.Connect();
            client.sendrecieve(client.makeData(CommandEnum.GetConfigCommand));

            GUITCPClient client2 = GUITCPClient.Instance;
            client2.Connect();
            client2.sendrecieve(client2.makeData(CommandEnum.LogCommand));
            Console.ReadLine();
            */
            #region test client
            
            foreach (string path in Directory.GetFiles(@"C:\Barak\biu\second year\semester d\Advanced Programing 2\Ex1\MyNewServiceSol\WebApplication2\Images", "*", SearchOption.AllDirectories))
            {
                Console.WriteLine(path);
            }
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