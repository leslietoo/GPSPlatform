using System;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x04 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x04()
        {
        }

        public JT808LocationAttachImpl0x04(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 需要人工确认报警事件的 ID，从 1 开始计数
        /// </summary>
        public int EventId { get; set; }

        public override byte AttachInfoId { get; protected set; } = 0x04;

        public override byte AttachInfoLength { get; protected set; } = 2;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            EventId = Buffer.Span.ReadIntH2L(0, AttachInfoLength);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer= new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(EventId, 2, AttachInfoLength);
        }
    }
}
