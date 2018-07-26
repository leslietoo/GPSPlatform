using System;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x12Formatter))]
    public class JT808LocationAttachImpl0x12 : JT808LocationAttachBase
    {
        /// <summary>
        /// 位置类型
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        [Key(2)]
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// </summary>
        [Key(3)]
        public int AreaId { get; set; }

        /// <summary>
        /// 方向 
        /// 0：进
        /// 1：出
        /// </summary>
        [Key(4)]
        public JT808DirectionType Direction { get; set; }
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x12;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 6;
    }
}
