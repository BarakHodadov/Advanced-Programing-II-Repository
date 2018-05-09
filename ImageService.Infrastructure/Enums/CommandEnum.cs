using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infrastructure
{
    // enum for the commands types.
    public enum CommandEnum : int
    {
        NewFileCommand,
        GetConfigCommand,
        LogCommand,
        CloseCommand
    }
}
