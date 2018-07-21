using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public interface IService
    {
        string ServiceName { get; }
        void Start();
        void Stop();
    }
}
