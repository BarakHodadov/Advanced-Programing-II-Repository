using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using System.IO;

namespace ImageService.Controller.Handlers
{
    public interface IDirectoryHandler
    {
        event EventHandler DirectoryClosed;              // The Event That Notifies that the Directory is being closed
        void StartHandleDirectory();                            // The Function Recieves the directory to Handle
        void OnCommandRecieved(ICommand command);               // The Event that will be activated upon new Command
    }
}
