using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;
using ImageService.Commands;
using System.Threading;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        #endregion

        public event EventHandler DirectoryClosed;
        public DirectoyHandler(string path, IImageController imageController, ILoggingService loggingService)
        {
            this.m_path = path;
            this.m_controller = imageController;
            this.m_logging = loggingService;
            this.m_dirWatcher = new FileSystemWatcher(path, "*.*");
            
            this.m_dirWatcher.Created += OnChanged;
            this.DirectoryClosed += CloseHandler;
        }

        // called when the handler get a command
        public void OnCommandRecieved(ICommand command)
        {
            string message = "Handler recieved command.";
            this.m_logging.Log(message, MessageTypeEnum.INFO); // write to the log.

            // check if it a close command
            if (command is CloseCommand)
            {
                this.CloseHandler(this, EventArgs.Empty); // close the handler
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            string[] args = { e.FullPath };
            string msg = this.m_controller.ExecuteCommand(1, args, out bool result);

            this.m_logging.Log(msg, MessageTypeEnum.INFO);
        }

        public void StartHandleDirectory()
        {
            this.m_dirWatcher.EnableRaisingEvents = true;
        }

        // this function closes the handler.
        public void CloseHandler(object sender, EventArgs e)
        {
            AppConfigReader.Instance.removeHandler(this.m_path);

            this.m_logging.Log("The handler cloesd successfuly", MessageTypeEnum.INFO);
            this.m_dirWatcher.EnableRaisingEvents = false;
            this.m_dirWatcher.Dispose();
        }
    }
}
