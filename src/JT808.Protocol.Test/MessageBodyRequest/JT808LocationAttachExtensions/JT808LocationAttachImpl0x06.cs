using System;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using Protocol.Common.Extensions;
using MessagePack;
using JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;

namespace JT808.Protocol.Test.JT808LocationAttach
{
    /// <summary>
    /// 自定义附加信息
    /// Age-word-2
    /// UserName-BCD(10)
    /// Gerder-byte-1
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x06Formatter))]
    public class JT808LocationAttachImpl0x06: JT808LocationAttachBase
    {
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x06;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 13;
        [Key(2)]
        public int Age { get; set; }
        [Key(3)]
        public byte Gender { get; set; }
        [Key(4)]
        public string UserName { get; set; }
    }
}
