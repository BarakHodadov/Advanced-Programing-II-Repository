using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;

namespace ImageService.Infrastructure
{
    public sealed class JsonConvertor
    {
        private static JsonConvertor instance = null;
        private static readonly object my_lock = new object();
        private JsonConvertor()
        {
        }

        public static JsonConvertor Instance
        {
            get
            {
                lock (my_lock)
                {
                    if (instance == null)
                    {
                        instance = new JsonConvertor();
                    }
                    return instance;
                }
            }
        }
        public string ToJson(ICommand command)
        {
            string str = JsonConvert.SerializeObject(command, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return str;
        }
        public ICommand FromJson(string commandStr)
        {
            ICommand command = JsonConvert.DeserializeObject<ICommand>(commandStr, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return command;
        }
    }
}
