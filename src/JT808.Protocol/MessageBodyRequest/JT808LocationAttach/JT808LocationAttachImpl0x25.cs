using System;
using Protocol.Common.Extensions;


namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x25 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x25()
        {
        }

        public JT808LocationAttachImpl0x25(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 扩展车辆信号状态位
        /// </summary>
        public int CarSignalStatus { get; set; }

        public override byte AttachInfoId { get; protected set; } = 0x25;

        public override byte AttachInfoLength { get; protected set; } = 4;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            CarSignalStatus = Buffer.Span.ReadIntH2L(2, 4);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer= new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(CarSignalStatus, 2, 4);
        }
    }
}
