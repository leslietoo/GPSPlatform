using System;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x2A : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x2A()
        {
        }

        public JT808LocationAttachImpl0x2A(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// IO状态位
        /// </summary>
        public int IOStatus { get; set; }

        public override byte AttachInfoId { get; protected set; } = 0x2A;

        public override byte AttachInfoLength { get; protected set; } = 2;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            IOStatus = Buffer.Span.ReadIntH2L(2, 2);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer= new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(IOStatus, 2, 2);
        }
    }
}
