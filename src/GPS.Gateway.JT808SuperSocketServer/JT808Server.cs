using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808Server : AppServer<JT808Session<JT808RequestInfo>, JT808RequestInfo>
    {
        public JT808Server(): base(new DefaultReceiveFilterFactory<JT808ReceiveFilter, JT808RequestInfo>())
        {

        }
    }
}
