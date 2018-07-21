using System;

namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 终端注销请求
    /// </summary>
    public class JT808_0x0002 : JT808Bodies
    {
        public JT808_0x0002()
        {
        }

        public JT808_0x0002(Memory<byte> buffer) : base(buffer)
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
