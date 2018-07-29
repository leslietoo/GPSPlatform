using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using MessagePack;
using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.MessageBodyReply
{
    /// <summary>
    /// 终端通用应答
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0001Formatter))]
    public class JT808_0x0001 : JT808Bodies
    {

        /// <summary>
        /// 应答流水号
        /// 对应的平台消息的流水号
        /// </summary>
        [Key(0)]
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 应答 ID
        /// 对应的平台消息的 ID
        /// </summary>
        [Key(1)]
        public JT808MsgId MsgId { get; set; }
        /// <summary>
        /// 结果
        /// 0：成功/确认；1：失败；2：消息有误；3：不支持
        /// </summary>
        [Key(2)]
        public JT808TerminalResult JT808TerminalResult { get; set; }
    }
}
