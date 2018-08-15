using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBodyReply
{
    public class JT808_0x8001Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            // 00 01 01 02 00
            JT808_0x8001 jT808PlatformReplyProperty = new JT808_0x8001();
            jT808PlatformReplyProperty.JT808PlatformResult = JT808PlatformResult.Success;
            jT808PlatformReplyProperty.MsgId = JT808MsgId.终端鉴权;
            jT808PlatformReplyProperty.MsgNum = 1;
            string hex = JT808Serializer.Serialize(jT808PlatformReplyProperty).ToHexString();
        }

        [Fact]
        public void Test1_2()
        {
            byte[] bytes = "00 01 01 02 00".ToHexBytes();
            JT808_0x8001 jT808PlatformReply = JT808Serializer.Deserialize<JT808_0x8001>(bytes);
            Assert.Equal(JT808MsgId.终端鉴权,jT808PlatformReply.MsgId);
            Assert.Equal(1, jT808PlatformReply.MsgNum);
            Assert.Equal(JT808PlatformResult.Success, jT808PlatformReply.JT808PlatformResult);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E 80 01 00 05 01 38 12 34 56 78 00 85 00 7B 01 02 00 48 7E".ToHexBytes();
            JT808Package jT808Package = JT808Serializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(JT808MsgId.平台通用应答, jT808Package.Header.MsgId);
            Assert.Equal(133, jT808Package.Header.MsgNum);
            Assert.Equal(JT808PlatformResult.Success, ((JT808_0x8001)jT808Package.Bodies).JT808PlatformResult);
            Assert.Equal(123, ((JT808_0x8001)jT808Package.Bodies).MsgNum);
            Assert.Equal(JT808MsgId.终端鉴权, ((JT808_0x8001)jT808Package.Bodies).MsgId);
        }
    }
}
