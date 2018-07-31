using Microsoft.Extensions.Logging;
using SuperSocket.SocketBase.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class SuperSocketNLogFactoryExtensions : ILogFactory
    {
        private readonly Microsoft.Extensions.Logging.ILoggerFactory LogFactory;

        public SuperSocketNLogFactoryExtensions(Microsoft.Extensions.Logging.ILoggerFactory logFactory)
        {
            LogFactory = logFactory;
        }

        public ILog GetLog(string name)
        {
            return new SuperSocketNLogExtensions(LogFactory.CreateLogger(name));
        }
    }
}
