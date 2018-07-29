using System;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x11Formatter))]
    public class JT808LocationAttachImpl0x11 : JT808LocationAttachBase
    {
        /// <summary>
        /// 超速报警附加信息
        /// 0：无特定位置；
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        [Key(2)]
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// 若位置类型为 0，无该字段
        /// </summary>
        [Key(3)]
        public int AreaId { get; set; }
        [Key(0)]
        public override byte AttachInfoId { get; set; } = 0x11;
        [Key(1)]
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
            set { }
        }
    }
}
