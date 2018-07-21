using System;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x01 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x01()
        {
        }

        public JT808LocationAttachImpl0x01(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 里程
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// 里程 1/10km，对应车上里程表读数
        /// </summary>
        public double ConvertMileage => Mileage / 10;

        public override byte AttachInfoId { get; protected set; } = 0x01;

        public override byte AttachInfoLength { get; protected set; } = 4;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            Mileage = Buffer.Span.ReadIntH2L(2, AttachInfoLength);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer = new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(Mileage, 2, AttachInfoLength);
        }
    }
}
