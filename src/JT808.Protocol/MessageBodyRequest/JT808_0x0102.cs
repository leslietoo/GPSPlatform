using MessagePack;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;

namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 终端鉴权
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0102Formatter))]
    public class JT808_0x0102 : JT808Bodies
    {
        /// <summary>
        /// 鉴权码
        /// </summary>
        [Key(0)]
        public string Code { get; set; }
    }
}
