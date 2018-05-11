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
        public override string ToString()
        {
            return this.type + " " + this.Message;
        }

        public string Color
        {
            get { return this.getColor(); }
        }

        public string getColor()
        {
            switch(this.type)
            {
                case "Info":
                    return "#FF3FCD16";
                case "Warning":
                    return "Yellow";
                case "Error":
                    return "Red";
                default:
                    return "White";
            }
        }
    }
}
