using System;
using System.Collections.Generic;
using System.Text;
using Protocol.Common.Extensions;
using JT808.Protocol.Enums;

namespace JT808.Protocol.MessageBodyReply
{
    /// <summary>
    /// 终端注册应答
    /// </summary>
    public class JT808_0x8100 : JT808Bodies
    {
        public JT808_0x8100()
        {
        }

        public JT808_0x8100(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 应答流水号
        /// 对应的终端注册消息的流水号
        /// </summary>
        public int MsgNum { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public JT808TerminalRegisterResult JT808TerminalRegisterResult { get; set; }

        /// <summary>
        /// 鉴权码
        /// 只有在成功后才有该字段
        /// </summary>
        public string Code { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            MsgNum = Buffer.Span.ReadIntH2L(0, 2);
            JT808TerminalRegisterResult = (JT808TerminalRegisterResult)Buffer.Span[2];
            // 只有在成功后才有该字段
            if (JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                Code = Buffer.Span.Slice(3).ReadStringLittle(0);
            }
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            if (JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                Span<byte> codeSpan = jT808GlobalConfigs.JT808Encoding.GetBytes(Code);
                Buffer = new byte[2 + 1 + codeSpan.Length];
                Buffer.Span.WriteLittle(MsgNum, 0, 2);
                Buffer.Span.WriteLittle((byte)JT808TerminalRegisterResult, 2);
                Buffer.Span.WriteLittle(codeSpan, 3);
            }
            else
            {
                Buffer = new byte[2 + 1];
                Buffer.Span.WriteLittle(MsgNum, 0, 2);
                Buffer.Span.WriteLittle((byte)JT808TerminalRegisterResult, 2);
            }
        }
    }
}
