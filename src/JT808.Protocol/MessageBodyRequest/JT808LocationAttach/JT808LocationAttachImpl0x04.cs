using System;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x04Formatter))]
    public class JT808LocationAttachImpl0x04 : JT808LocationAttachBase
    {
        /// <summary>
        /// 需要人工确认报警事件的 ID，从 1 开始计数
        /// </summary>
        [Key(2)]
        public ushort EventId { get; set; }
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x04;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 2;
    }
}
