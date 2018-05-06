using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    public class Log
    {
        private string type;
        private string message;

        public Log(string type, string message)
        {
            this.type = type;
            this.message = message;
        }
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
    }
    public class Logger
    {
        private List<Log> logs;
        public Logger()
        {
            logs = new List<Log>();
        }
        public List<Log> Logs
        {
            get { return this.Logs; }
        }
        public void addLog(object source, Logging.Modal.MessageRecievedEventArgs e)
        {
            Log l = new Log(e.Status.ToString(), e.Message);
            this.logs.Add(l);
        }
    }
}
