using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Protocol.Common.Extensions;

namespace JT808.Protocol.Test
{
    public class JT808HeaderTest: JT808PackageBase
    {
        [Fact]
        public void Test3()
        {
            ReadOnlySpan<char> dataLen = Convert.ToString(5, 2).PadLeft(10, '0').AsSpan();
        }

        [Fact]
        public void Test4()
        {
            // "0000000000000101"
            short a = Convert.ToInt16("0000000000000101",2);
            var msgMethodBytes = BitConverter.GetBytes(a);

        }

        [Fact]
        public void Test1()
        {
            JT808Header jT808HeaderProperty = new JT808Header();
            jT808HeaderProperty.TerminalPhoneNo = "013812345678";
            jT808HeaderProperty.IsPackge = false;
            jT808HeaderProperty.MsgNum = 135;
            jT808HeaderProperty.MsgId = JT808MsgId.终端鉴权;
            jT808HeaderProperty.DataLength = 5;
            jT808HeaderProperty.WriteBuffer(jT808GlobalConfigs);
            var hex = jT808HeaderProperty.Buffer.Span.ToArray().ToHexString();
        }

        [Fact]
        public void Test1_2()
        {
            byte[] headerBytes = "01 02 00 05 01 38 12 34 56 78 00 87 00".ToHexBytes();
            JT808Header jT808Header = new JT808Header(headerBytes);
            jT808Header.ReadBuffer(jT808GlobalConfigs);
            Assert.Equal("013812345678", jT808Header.TerminalPhoneNo);
            Assert.False(jT808Header.IsPackge);
            Assert.Equal(JT808MsgId.终端鉴权, jT808Header.MsgId);
            Assert.Equal(5, jT808Header.DataLength);
        }
    }
}
