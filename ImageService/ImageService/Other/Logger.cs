using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    public class Logger
    {
        private List<Log> logs;
        public Logger()
        {
            logs = new List<Log>();
        }
        public List<Log> Logs
        {
            get { return this.logs; }
        }
        public void addLog(object source, Logging.Modal.MessageRecievedEventArgs e)
        {
            Log l = new Log(e.Status.ToString(), e.Message);
            this.logs.Add(l);
        }
    }
}
