using JT808.Protocol;
using SuperSocket.Facility.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808ReceiveFilter : BeginEndMarkReceiveFilter<JT808RequestInfo>
    {
        static readonly byte[] beginMark = new byte[] { JT808Package.BeginFlag };

        static readonly byte[] endMark = new byte[] { JT808Package.EndFlag };

        public JT808ReceiveFilter() : base(beginMark, endMark)
        {
        }

        protected override JT808RequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {
            // 防止socket字节流攻击  
            if (length < 3)
            {
                base.Reset();
                return null;
            }
            return new JT808RequestInfo(readBuffer);
        }
    }
}
