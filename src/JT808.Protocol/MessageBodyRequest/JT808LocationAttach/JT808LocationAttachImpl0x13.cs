using System;
using JT808.Protocol.Enums;
using Protocol.Common.Extensions;


namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x13 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x13()
        {
        }

        public JT808LocationAttachImpl0x13(Memory<byte> buffer) : base(buffer)
        {
        }

        public override byte AttachInfoId { get; protected set; } = 0x13;

        public override byte AttachInfoLength { get; protected set; } = 7;

        /// <summary>
        /// 路段 ID
        /// </summary>
        public int DrivenRouteId { get; set; }

        /// <summary>
        /// 路段行驶时间
        /// 单位为秒（s)
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        ///  结果 0：不足；1：过长
        /// </summary>
        public JT808DrivenRouteType DrivenRoute { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            DrivenRouteId = Buffer.Span.ReadIntH2L(2, 4);
            Time = Buffer.Span.ReadIntH2L(6, 2);
            DrivenRoute = (JT808DrivenRouteType)Buffer.Span[8];
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer= new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(DrivenRouteId, 2, 4);
            Buffer.Span.WriteLittle(Time, 6, 2);
            Buffer.Span.WriteLittle((byte)DrivenRoute, 8);
        }
    }
}
