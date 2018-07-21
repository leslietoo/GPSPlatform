using System;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x2B : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x2B()
        {
        }

        public JT808LocationAttachImpl0x2B(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 模拟量 bit0-15，AD0；bit16-31，AD1
        /// </summary>
        public int Analog { get; set; }

        public override byte AttachInfoId { get; protected set; } = 0x2B;

        public override byte AttachInfoLength { get; protected set; } = 4;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            Analog = Buffer.Span.ReadIntH2L(2, 4);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer = new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(Analog, 2, 4);
        }
    }
}
