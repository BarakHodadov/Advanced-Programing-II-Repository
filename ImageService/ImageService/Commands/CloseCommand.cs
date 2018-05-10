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
    public class CloseCommand : ICommand
    {
        List<IDirectoryHandler> handlers;

        // A constructor.
        public CloseCommand(List<IDirectoryHandler> handlers)
        {
            this.handlers = handlers;
        }

        public string Execute(string[] args, out bool result)
        {
            // send a message to the handler that this command needs to be executed.
            foreach (IDirectoryHandler handler in this.handlers)
            {
                if (handler.getPath() == args[0])
                {
                    handler.OnCommandRecieved(this);
                }
            }
            
            //this.directoryHandler.DirectoryClosed?.Invoke();
            //AppConfigReader.Instance.removeHandler(args[0]);
            result = true;
            return "";
        }
    }
}
