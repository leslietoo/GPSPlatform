using System;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x30Formatter))]
    public class JT808LocationAttachImpl0x30 : JT808LocationAttachBase
    {
        /// <summary>
        /// 无线通信网络信号强度
        /// </summary>
        [Key(2)]
        public byte WiFiSignalStrength { get; set; }
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x30;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 1;
    }
}
