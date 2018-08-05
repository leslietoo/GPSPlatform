using JT808.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using JT808.Protocol.Extensions;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808RequestInfo : IRequestInfo
    {
        public string Key => "JT808";

        public JT808Package JT808Package { get; }

        public byte[] OriginalBuffer { get; }

        public JT808RequestInfo(byte[] buffer)
        {
            try
            {
                OriginalBuffer = buffer;
                JT808Package = JT808Serializer.Deserialize<JT808Package>(buffer);
            }
            catch (Exception ex)
            {
                JT808Package = null ;
            }
        }
    }
}
