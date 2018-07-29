using System;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x03Formatter))]
    public class JT808LocationAttachImpl0x03 : JT808LocationAttachBase
    {
        /// <summary>
        /// 行驶记录功能获取的速度
        /// </summary>
        [Key(2)]
        public ushort Speed { get; set; }

        /// <summary>
        /// 行驶记录功能获取的速度 1/10km/h
        /// </summary>
        [IgnoreMember]
        public double ConvertSpeed => Speed / 10.0;
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x03;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 2;
    }
}
