using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;

namespace ImageService.Controller.Handlers
{
    public interface IDirectoryHandler
    {
        void StartHandleDirectory();                            // The Function Recieves the directory to Handle
        void OnCommandRecieved(ICommand command);               // The Event that will be activated upon new Command
    }
}
