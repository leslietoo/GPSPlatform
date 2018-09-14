using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GPS.JT808NettyServer
{
    public interface IAppSession
    {
        string SessionID { get; }

        IChannel Channel { get; }

        DateTime LastActiveTime { get; set; }

        DateTime StartTime { get; }
    }
}
