using System;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x03 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x03()
        {
        }

        public JT808LocationAttachImpl0x03(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 行驶记录功能获取的速度
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 行驶记录功能获取的速度 1/10km/h
        /// </summary>
        public double ConvertSpeed => Speed / 10.0;

        public override byte AttachInfoId { get; protected set; } = 0x03;

        public override byte AttachInfoLength { get; protected set; } = 2;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            Speed = Buffer.Span.ReadIntH2L(2, AttachInfoLength);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer= new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(Speed, 2, AttachInfoLength);
        }
    }
}
