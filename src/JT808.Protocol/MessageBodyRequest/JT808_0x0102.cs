using Protocol.Common.Extensions;
using JT808.Protocol.JT808RequestProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 终端鉴权
    /// </summary>
    public class JT808_0x0102 : JT808Bodies
    {
        public JT808_0x0102()
        {
        }

        public JT808_0x0102(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 鉴权码
        /// </summary>
        public string Code { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Code = Buffer.Span.ReadStringLittle(0, Buffer.Length);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer = jT808GlobalConfigs.JT808Encoding.GetBytes(Code);
        }
    }
}
