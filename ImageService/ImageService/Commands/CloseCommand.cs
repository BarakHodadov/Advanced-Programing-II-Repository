using ImageService.Commands;
using ImageService.Controller.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class CloseCommand : ICommand
    {
        IDirectoryHandler directoryHandler;

        public CloseCommand(IDirectoryHandler directoryHandler)
        {
            this.directoryHandler = directoryHandler;
        }

        public string Execute(string[] args, out bool result)
        {
            this.directoryHandler.OnCommandRecieved(this);
            result = true;
            return "";
        }
    }
}
