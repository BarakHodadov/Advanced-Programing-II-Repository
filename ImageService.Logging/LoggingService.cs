
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        // A default constructor.
        public LoggingService()
        {
        }

        //Log function.
        //Invoke message recieved event to write to the event log.
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecievedEventArgs msg = new MessageRecievedEventArgs(message, type);

            MessageRecieved?.Invoke(this, msg);
        }
    }
}
