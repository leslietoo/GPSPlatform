using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.MessageBodyReply;
using JT808.Protocol.Enums;
using Protocol.Common.Extensions;

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
            jT808PlatformReplyProperty.WriteBuffer(jT808GlobalConfigs);
            string hex = jT808PlatformReplyProperty.Buffer.Span.ToArray().ToHexString();
        }

        [Fact]
        public void Test1_2()
        {
            byte[] bytes = "00 01 01 02 00".ToHexBytes();
            JT808_0x8001 jT808PlatformReply = new JT808_0x8001(bytes);
            jT808PlatformReply.ReadBuffer(jT808GlobalConfigs);
            Assert.Equal(JT808MsgId.终端鉴权,jT808PlatformReply.MsgId);
            Assert.Equal(1, jT808PlatformReply.MsgNum);
            Assert.Equal(JT808PlatformResult.Success, jT808PlatformReply.JT808PlatformResult);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "7E 01 02 00 05 01 38 12 34 56 78 00 00 00 01 80 01 00 B7 00 00 7E".ToHexBytes();
            JT808Package jT808Package = new JT808Package(bytes);
            jT808Package.ReadBuffer(jT808GlobalConfigs);
        }

        [Fact]
        public void Test3()
        {
            byte[] bytes = "7E 80 01 00 05 01 38 12 34 56 78 00 01 00 8A 00 02 00 3C 7E".ToHexBytes();
            JT808Package jT808Package = new JT808Package(bytes);
            jT808Package.ReadBuffer(jT808GlobalConfigs);
        }

        [Fact]
        public void Test4()
        {
            //013510260001
            byte[] bytes = "7E 81 00 00 10 01 35 10 26 00 01 00 05 00 00 30 00 4A 30 31 33 35 31 30 32 36 30 30 30 EE 7E".ToHexBytes();
            JT808Package jT808Package = new JT808Package(bytes);
            jT808Package.ReadBuffer(jT808GlobalConfigs);
        }

        [Fact]
        public void Test5()
        {
            //01 35 10 26 00 01
            //Span<byte> bytes = "00 85 01 02 00 78".ToHexBytes();
            Span<byte> b = new byte[6];

            b.WriteBCDLittle("013812345678", 0, 6);
            string a= b.ToArray().ToHexString();
        }
    }
}
