using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Communication
{
    public interface IClient
    {
        void Connect();                     // Connect to the server.
        void Send(string data);             // Send data to the server.
        string Recieve();                   // Recieve data from the server.
        void Disconnect();                  // Disconnect from the server.
        string sendrecieve(string data);
    }
}
