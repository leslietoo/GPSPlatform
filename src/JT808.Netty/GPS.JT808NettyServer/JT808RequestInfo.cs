using JT808.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808NettyServer
{
    public class JT808RequestInfo
    {
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
                JT808Package = null;
            }
        }
    }
}
