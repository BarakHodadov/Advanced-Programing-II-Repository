﻿using ImageService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;

namespace ImageService.Commands
{
    public class LogCommand : ICommand
    {
        private Logger logger;
        public LogCommand(Logger logger)
        {
            this.logger = logger;
        }

        public string Execute(string[] args, out bool result)
        {
            result = true;
            string logs = "INFO#info message;WARNING#warning message;ERROR#error message;";
            foreach(Log l in this.logger.Logs)
            {
                logs += l.Type + "#" + l.Message + ";";
            }
            return logs;
        }
    }
}
