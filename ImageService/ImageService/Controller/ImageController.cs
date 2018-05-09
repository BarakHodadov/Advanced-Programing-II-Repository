using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;

        public ImageController(IImageServiceModal modal, Logger logs)
        {
            m_modal = modal;             // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>();

            commands.Add(1, new NewFileCommand(m_modal));
            commands.Add(2, new GetConfigCommand());
            commands.Add(3, new LogCommand(logs));
        }

        // the function gets a command id and arguments and executes it.
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            string str = commands[commandID].Execute(args, out resultSuccesful);
            return str;       
        }
    }
}
