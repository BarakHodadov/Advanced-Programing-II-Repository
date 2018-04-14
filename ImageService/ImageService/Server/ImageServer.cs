using ImageService.Commands;
using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        private List<string> pathsToListen;
        private List<IDirectoryHandler> handlersList;
        #endregion
       
        // A constructor.
        public ImageServer(IImageController imageController, ILoggingService loggingService, List<string> paths)
        {
            this.m_controller = imageController;
            this.m_logging = loggingService;
            this.pathsToListen = paths;
            this.handlersList = new List<IDirectoryHandler>();
        }

        public void CreateHandlers()
        {
            // goes through all directories in the list and if it exists than creates a new handler and handles the directory.
            foreach(string directory in pathsToListen)
            {
                if (Directory.Exists(directory))
                {
                    IDirectoryHandler handler = new DirectoyHandler(directory, this.m_controller, this.m_logging); // create handler
                    handlersList.Add(handler); // adds to the list
                    handler.StartHandleDirectory(); // starting handle the directory
                    this.m_logging.Log("Add a handler",Logging.Modal.MessageTypeEnum.INFO); // sending message to the log file
                    
                }
            }
        }

        // this function is called when the server is been closed
        public void OnCloseServer(Object sender, EventArgs e)
        {
            List<IDirectoryHandler> tempList = new List<IDirectoryHandler>(handlersList);
            // goes through all handlers and tells them that the server is been closed.
           foreach (IDirectoryHandler handler in tempList)
            {
                handler.OnCommandRecieved(new CloseCommand(handler)); // creates a new close command and handles it.
            }
        }
    }
}
