
using Xunit;
using JT808.Protocol.MessageBodyRequest;
using Protocol.Common.Extensions;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0102Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            //45612
            JT808_0x0102 jT808LoginRequestProperty = new JT808_0x0102();
            jT808LoginRequestProperty.Code = "45612";
            jT808LoginRequestProperty.WriteBuffer(jT808GlobalConfigs);
            string hex= jT808LoginRequestProperty.Buffer.ToArray().ToHexString();
        }
        [Fact]
        public void Test2()
        {
            byte[] bodys = "34 35 36 31 32".ToHexBytes();
            JT808_0x0102 jT808LoginRequest = new JT808_0x0102(bodys);
            jT808LoginRequest.ReadBuffer(jT808GlobalConfigs);
            Assert.Equal("45612", jT808LoginRequest.Code);
        }

        [Fact]
        public void Test3()
        {
            byte[] bodys = "7E 01 02 00 04 01 35 10 26 00 01 00 34 00 00 00 00 30 7E".ToHexBytes();
            JT808Package jT808LoginRequest = new JT808Package(bodys);
            jT808LoginRequest.ReadBuffer(jT808GlobalConfigs);
        }

        [Fact]
        public void Test4()
        {
            byte[] bodys = "7E 01 02 00 0E 01 35 10 26 00 01 01 8C 4A 30 31 33 35 31 30 32 36 30 30 30 31 00 CA 7E".ToHexBytes();
            JT808Package jT808LoginRequest = new JT808Package(bodys);
            jT808LoginRequest.ReadBuffer(jT808GlobalConfigs);
        }
    }
}
