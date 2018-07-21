using System;
using System.Collections.Generic;
using System.Text;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodySend
{
    /// <summary>
    /// 文本信息下发
    /// </summary>
    public class JT808_0x8300 : JT808Bodies
    {
        public JT808_0x8300()
        {
        }

        public JT808_0x8300(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 文本信息标志位含义见 表 38
        /// </summary>
        public byte TextFlag { get; set; }

        /// <summary>
        /// 文本信息
        /// 最长为 1024 字节，经GBK编码
        /// </summary>
        public string TextInfo { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            TextFlag = Buffer.Span[0];
            TextInfo = Buffer.Span.ReadStringLittle(1,jT808GlobalConfigs.JT808Encoding);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(TextFlag);
            bytes.AddRange(jT808GlobalConfigs.JT808Encoding.GetBytes(TextInfo));
            Buffer = bytes.ToArray();
        }
    }
}
