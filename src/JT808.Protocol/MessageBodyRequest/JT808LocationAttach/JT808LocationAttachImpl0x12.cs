using System;
using JT808.Protocol.Enums;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x12 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x12()
        {
        }

        public JT808LocationAttachImpl0x12(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 位置类型
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 方向 
        /// 0：进
        /// 1：出
        /// </summary>
        public JT808DirectionType Direction { get; set; }

        public override byte AttachInfoId { get; protected set; } = 0x12;

        public override byte AttachInfoLength { get; protected set; } = 6;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            JT808PositionType = (JT808PositionType)Buffer.Span[2];
            AreaId = Buffer.Span.ReadIntH2L(3, 4);
            Direction = (JT808DirectionType)Buffer.Span[7];
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer= new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle((byte)JT808PositionType, 2);
            Buffer.Span.WriteLittle(AreaId, 3, 4);
            Buffer.Span.WriteLittle((byte)Direction, 7);
        }
    }
}
