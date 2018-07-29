using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x31Formatter))]
    public class JT808LocationAttachImpl0x31 : JT808LocationAttachBase
    {
        /// <summary>
        /// GNSS 定位卫星数
        /// </summary>
        [Key(2)]
        public byte GNSSCount { get; set; }
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x31;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 1;
    }
}
