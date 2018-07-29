using System;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;


namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x25Formatter))]
    public class JT808LocationAttachImpl0x25 : JT808LocationAttachBase
    {
        /// <summary>
        /// 扩展车辆信号状态位
        /// </summary>
        [Key(2)]
        public int CarSignalStatus { get; set; }
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x25;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 4;
    }
}
