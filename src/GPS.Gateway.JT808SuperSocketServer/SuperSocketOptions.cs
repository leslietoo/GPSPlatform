using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class SuperSocketOptions
    {
        public string IP { get; set; }

        public int Port { get; set; }

        public override string ToString()
        {
            return $"{IP}:{Port.ToString()}";
        }
    }
}
