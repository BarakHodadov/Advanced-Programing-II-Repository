using ImageService.Commands;
using ImageService.Controller.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;

namespace ImageService.Commands
{
    class CloseCommand : ICommand
    {
        IDirectoryHandler directoryHandler;

        // A constructor.
        public CloseCommand(IDirectoryHandler directoryHandler)
        {
            this.directoryHandler = directoryHandler;
        }

        public string Execute(string[] args, out bool result)
        {
            // send a message to the handler that this command needs to be executed.
            this.directoryHandler.OnCommandRecieved(this);

            //this.directoryHandler.DirectoryClosed?.Invoke();
            //AppConfigReader.Instance.removeHandler(args[0]);
            result = true;
            return "";
        }
    }
}
