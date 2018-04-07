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
       
        public ImageServer(IImageController imageController, ILoggingService loggingService, List<string> paths)
        {
            this.m_controller = imageController;
            this.m_logging = loggingService;
            this.pathsToListen = paths;
            this.handlersList = new List<IDirectoryHandler>();
        }

        public void CreateHandlers()
        {
            foreach(string directory in pathsToListen)
            {
                if (Directory.Exists(directory))
                {
                    IDirectoryHandler handler = new DirectoyHandler(directory, this.m_controller, this.m_logging);
                    handler.StartHandleDirectory();
                    handlersList.Add(handler);
                }
            }
        }

        public void OnCloseServer(IDirectoryHandler sender)
        {
           foreach (IDirectoryHandler handler in handlersList)
            {
                handler.OnCommandRecieved(new CloseCommand(handler));
            }
        }
       
    }
}
