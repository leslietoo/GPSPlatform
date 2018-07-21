using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 终端注销请求
    /// </summary>
    public class JT808_0x0003 : JT808Bodies
    {
        public JT808_0x0003()
        {
        }

        public JT808_0x0003(Memory<byte> buffer) : base(buffer)
        {
        }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
     
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
     
        }
    }
}
