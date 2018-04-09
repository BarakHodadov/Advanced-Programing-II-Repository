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

        public event EventHandler<FileSystemEventArgs> DirectoryChanged;
        public DirectoyHandler(string path, IImageController imageController, ILoggingService loggingService)
        {
            this.m_path = path;
            this.m_controller = imageController;
            this.m_logging = loggingService;
            this.m_dirWatcher = new FileSystemWatcher(path, "*.*");

            this.DirectoryChanged += OnChanged;
            this.m_dirWatcher.Changed += OnChanged;
        }

        public void OnCommandRecieved(ICommand command)
        {
            string message = "Handler recieved command.";
            this.m_logging.Log(message, MessageTypeEnum.INFO);

            if (command is CloseCommand)
            {
                this.CloseHandler();
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            string[] args = { e.FullPath };
            Console.WriteLine("in OnChanged");
            string msg = this.m_controller.ExecuteCommand(1, args, out bool result);

            this.m_logging.Log(msg, MessageTypeEnum.INFO);
        }

        public void StartHandleDirectory()
        {
            this.m_dirWatcher.EnableRaisingEvents = true;
        }

        public void CloseHandler()
        {
            this.m_dirWatcher.EnableRaisingEvents = false;
            this.m_dirWatcher.Dispose();
        }
    }
}
