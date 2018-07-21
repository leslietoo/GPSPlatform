using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Protocol.Common.Extensions;
using JT808.Protocol.MessageBodySend;

namespace JT808.Protocol.Test.MessageBodySend
{
    public class JT808_0x8300Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x8300 jT808TextSend = new JT808_0x8300();
            jT808TextSend.TextInfo = "smallchi 518";
            jT808TextSend.TextFlag = 5;
            jT808TextSend.WriteBuffer(jT808GlobalConfigs);
            string hex = jT808TextSend.Buffer.ToArray().ToHexString();
        }

        [Fact]
        public void Test1_2()
        {
            byte[] bytes = "05 73 6D 61 6C 6C 63 68 69 20 35 31 38".ToHexBytes();
            JT808_0x8300 jT808TextSend = new JT808_0x8300(bytes);
            jT808TextSend.ReadBuffer(jT808GlobalConfigs);
            Assert.Equal("smallchi 518", jT808TextSend.TextInfo);
            Assert.Equal(5, jT808TextSend.TextFlag);
        }
    }
}
