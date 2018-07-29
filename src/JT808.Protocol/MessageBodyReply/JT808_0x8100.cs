using System;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using MessagePack;

namespace JT808.Protocol.MessageBodyReply
{
    /// <summary>
    /// 终端注册应答
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x8100Formatter))]
    public class JT808_0x8100 : JT808Bodies
    {
        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        [Key(0)]
        public ushort MsgNum { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        [Key(1)]
        public JT808TerminalRegisterResult JT808TerminalRegisterResult { get; set; }

        /// <summary>
        /// 鉴权码
        /// 只有在成功后才有该字段
        /// </summary>
        [Key(3)]
        public string Code { get; set; }
    }
}
