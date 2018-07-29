using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Protocol.Common.Extensions;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public  class JT808_0x0002Test
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                 MsgId= Enums.JT808MsgId.终端心跳,
                 MsgNum=10,
                 TerminalPhoneNo="012345678900",
            };
            //"7E 00 02 00 00 01 23 45 67 89 00 00 0A 81 7E"
            var hex = MessagePackSerializer.Serialize(jT808Package).ToHexString();
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 00 02 00 00 01 23 45 67 89 00 00 0A 81 7E".ToHexBytes();
            JT808Package jT808Package = MessagePackSerializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端心跳, jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("012345678900", jT808Package.Header.TerminalPhoneNo);
            Assert.Null(jT808Package.Bodies);
        }
    }
}
