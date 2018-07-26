using System;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x2BFormatter))]
    public class JT808LocationAttachImpl0x2B : JT808LocationAttachBase
    {
        /// <summary>
        /// 模拟量 bit0-15，AD0；bit16-31，AD1
        /// </summary>
        [Key(2)]
        public int Analog { get; set; }
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x2B;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 4;
    }
}
