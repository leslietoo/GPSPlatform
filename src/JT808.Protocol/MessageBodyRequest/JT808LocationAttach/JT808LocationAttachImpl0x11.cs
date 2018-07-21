using System;
using JT808.Protocol.Enums;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x11 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x11()
        {
        }

        public JT808LocationAttachImpl0x11(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 超速报警附加信息
        /// 0：无特定位置；
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// 若位置类型为 0，无该字段
        /// </summary>
        public int AreaId { get; set; }

        public override byte AttachInfoId { get; protected set; } = 0x11;

        public override byte AttachInfoLength
        {
            get
            {
                if (JT808PositionType != JT808PositionType.无特定位置)
                {
                    return 5;
                }
                else
                {
                    return 1;
                }
            }
            protected set { }
        }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            JT808PositionType = (JT808PositionType)Buffer.Span[2];
            if (JT808PositionType != JT808PositionType.无特定位置)
            {
                AreaId = Buffer.Span.ReadIntH2L(3, 4);
            }
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer = new byte[1 + 1 + AttachInfoLength];
            if (JT808PositionType == JT808PositionType.无特定位置)
            {
                Buffer.Span.WriteLittle(AttachInfoId, 0);
                Buffer.Span.WriteLittle(AttachInfoLength, 1);
                Buffer.Span.WriteLittle((byte)JT808PositionType, 2);
            }
            else
            {
                Buffer.Span.WriteLittle(AttachInfoId, 0);
                Buffer.Span.WriteLittle(AttachInfoLength, 1);
                Buffer.Span.WriteLittle((byte)JT808PositionType, 2);
                Buffer.Span.WriteLittle(AreaId, 3, 4);
            }
        }
    }
}
