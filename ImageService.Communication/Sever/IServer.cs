using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    public interface IServer
    {
        event execute OnCommandRecieved;
        void Start();
        void Stop();
    }
}
