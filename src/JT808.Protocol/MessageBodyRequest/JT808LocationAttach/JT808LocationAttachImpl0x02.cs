using System;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x02Formatter))]
    public class JT808LocationAttachImpl0x02 : JT808LocationAttachBase
    {
        /// <summary>
        /// 油量
        /// </summary>
        [Key(2)]
        public ushort Oil { get; set; }
        /// <summary>
        /// 油量 1/10L，对应车上油量表读数
        /// </summary>
        [IgnoreMember]
        public double ConvertOil => Oil / 10.0;
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x02;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 2;
    }
}
