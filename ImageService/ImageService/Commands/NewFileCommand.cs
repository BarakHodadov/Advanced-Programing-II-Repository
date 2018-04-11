using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal m_modal;

        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }

        /*
         * converts the data that we will send to AddFile(...)
         * suppose that args[0] is sourcePath    args[1] is destPath
         * result is true if succedded and false otherwise
         */
        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and will return the error message
            string msg = m_modal.AddFile(args[0], out result);
            if (!result)
                return msg;
            return args[0];
        }
    }
}
