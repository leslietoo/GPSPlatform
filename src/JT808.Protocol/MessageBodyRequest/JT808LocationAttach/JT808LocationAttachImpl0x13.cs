using System;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;


namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x13Formatter))]
    public class JT808LocationAttachImpl0x13 : JT808LocationAttachBase
    {
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x13;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 7;

        /// <summary>
        /// 路段 ID
        /// </summary>
        [Key(2)]
        public int DrivenRouteId { get; set; }

        /// <summary>
        /// 路段行驶时间
        /// 单位为秒（s)
        /// </summary>
        [Key(3)]
        public ushort Time { get; set; }

        /// <summary>
        ///  结果 0：不足；1：过长
        /// </summary>
        [Key(4)]
        public JT808DrivenRouteType DrivenRoute { get; set; }
    }
}
