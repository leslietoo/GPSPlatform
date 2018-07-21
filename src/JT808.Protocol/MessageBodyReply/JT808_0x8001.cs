using JT808.Protocol.Enums;
using Protocol.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBodyReply
{
    /// <summary>
    /// 平台通用应答
    /// </summary>
    public class JT808_0x8001 : JT808Bodies
    {
        public JT808_0x8001()
        {
        }

        public JT808_0x8001(Memory<byte> buffer) : base(buffer)
        {
        }

        public int MsgNum { get; set; }

        public JT808MsgId MsgId { get; set; }

        public JT808PlatformResult JT808PlatformResult { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            MsgNum = Buffer.Span.ReadIntH2L(0, 2);
            MsgId = (JT808MsgId)Buffer.Span.ReadIntH2L(2, 2);
            JT808PlatformResult = (JT808PlatformResult)Buffer.Span.ReadIntH2L(4, 1);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer = new byte[5];
            Buffer.Span.WriteLittle(MsgNum, 0, 2);
            Buffer.Span.WriteLittle((int)MsgId, 2, 2);
            Buffer.Span.WriteLittle((byte)JT808PlatformResult, 4);
        }
    }
}
