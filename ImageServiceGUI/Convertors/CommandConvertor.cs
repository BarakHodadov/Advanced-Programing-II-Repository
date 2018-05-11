using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;

namespace ImageService.Infrastructure
{
    public class CommandConvertor
    {
        private static CommandConvertor instance = null;
        private static readonly object my_lock = new object();
        private CommandConvertor()
        {
        }
        public static CommandConvertor Instance
        {
            get
            {
                lock (my_lock)
                {
                    if (instance == null)
                    {
                        instance = new CommandConvertor();
                    }
                    return instance;
                }
            }
        }
        public int ConvertCommandToID(CommandEnum cmd)
        {
            int commandID = (int)cmd;
            switch (commandID)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
                case 3:
                    return 4;
                default:
                    return 0;
            }
        }
    }
}