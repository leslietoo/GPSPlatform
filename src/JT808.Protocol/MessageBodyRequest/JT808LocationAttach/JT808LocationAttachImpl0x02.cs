using System;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x02 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x02()
        {
        }

        public JT808LocationAttachImpl0x02(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 油量
        /// </summary>
        public int Oil { get; set; }
        /// <summary>
        /// 油量 1/10L，对应车上油量表读数
        /// </summary>
        public double ConvertOil => Oil / 10.0;

        public override byte AttachInfoId { get; protected set; } = 0x02;

        public override byte AttachInfoLength { get; protected set; } = 2;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength= Buffer.Span[1];
            Oil = Buffer.Span.ReadIntH2L(2, AttachInfoLength);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer = new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(Oil, 2, AttachInfoLength);
        }
    }
}
