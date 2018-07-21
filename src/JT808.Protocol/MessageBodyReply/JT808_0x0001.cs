using JT808.Protocol.Enums;
using Protocol.Common.Extensions;
using System;

namespace JT808.Protocol.MessageBodyReply
{
    /// <summary>
    /// 终端通用应答
    /// </summary>
    public class JT808_0x0001 : JT808Bodies
    {
        public JT808_0x0001()
        {
        }

        public JT808_0x0001(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 应答流水号
        /// 对应的平台消息的流水号
        /// </summary>
        public int MsgNum { get; set; }
        /// <summary>
        /// 应答 ID
        /// 对应的平台消息的 ID
        /// </summary>
        public JT808MsgId MsgId { get; set; }
        /// <summary>
        /// 结果
        /// 0：成功/确认；1：失败；2：消息有误；3：不支持
        /// </summary>
        public JT808TerminalResult JT808TerminalResult { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            MsgNum = Buffer.Span.ReadIntH2L(0, 2);
            MsgId = (JT808MsgId)Buffer.Span.ReadIntH2L(2, 2);
            JT808TerminalResult = (JT808TerminalResult)Buffer.Span.ReadIntH2L(4, 1);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer= new byte[5];
            Buffer.Span.WriteLittle(MsgNum, 0, 2);
            Buffer.Span.WriteLittle((int)MsgId, 2, 2);
            Buffer.Span.WriteLittle((byte)JT808TerminalResult, 4);
        }
    }
}
