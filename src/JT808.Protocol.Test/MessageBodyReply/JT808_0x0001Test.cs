using JT808.Protocol.Enums;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBodyReply
{
    public class JT808_0x0001Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            // "00 01 00 01 00"
            JT808_0x0001 jT808TerminalReplyProperty = new JT808_0x0001();
            jT808TerminalReplyProperty.JT808TerminalResult = JT808TerminalResult.Success;
            jT808TerminalReplyProperty.MsgId = JT808MsgId.终端通用应答;
            jT808TerminalReplyProperty.MsgNum = 1;
            string hex = JT808Serializer.Serialize(jT808TerminalReplyProperty).ToHexString();
        }

        [Fact]
        public void Test1_2()
        {
            byte[] bytes = "00 01 00 01 00".ToHexBytes();
            JT808_0x0001 jT808TerminalReply = JT808Serializer.Deserialize<JT808_0x0001>(bytes);
            Assert.Equal(JT808MsgId.终端通用应答, jT808TerminalReply.MsgId);
            Assert.Equal(1, jT808TerminalReply.MsgNum);
            Assert.Equal(JT808TerminalResult.Success, jT808TerminalReply.JT808TerminalResult);
        }
    }
}
