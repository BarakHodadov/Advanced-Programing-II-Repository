using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging.Modal
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageRecievedEventArgs(string msg, MessageTypeEnum status)
        {
            this.Message = msg;
            this.Status = status;
        }
        public MessageTypeEnum Status { get; set; }
        public string Message { get; set; }
    }
}
