using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using MessagePack;

namespace JT808.Protocol.MessageBodyReply
{
    /// <summary>
    /// 平台通用应答
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x8001Formatter))]
    public class JT808_0x8001 : JT808Bodies
    {
        [Key(0)]
        public ushort MsgNum { get; set; }
        [Key(1)]
        public JT808MsgId MsgId { get; set; }
        [Key(2)]
        public JT808PlatformResult JT808PlatformResult { get; set; }
    }
}
