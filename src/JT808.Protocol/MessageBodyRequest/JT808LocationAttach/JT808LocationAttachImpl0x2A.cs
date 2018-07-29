using System;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x2AFormatter))]
    public class JT808LocationAttachImpl0x2A : JT808LocationAttachBase
    {
        /// <summary>
        /// IO状态位
        /// </summary>
        [Key(2)]
        public ushort IOStatus { get; set; }
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x2A;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 2;
    }
}
