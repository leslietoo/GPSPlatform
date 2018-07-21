using Xunit;
using JT808.Protocol.MessageBodyRequest;
using Protocol.Common.Extensions;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0100Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            byte[] bytes = "7E 01 00 00 2C 01 35 10 26 00 01 00 11 00 2C 01 33 37 30 39 36 30 41 35 45 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 30 34 31 36 39 30 30 01 D4 C1 42 38 38 38 38 44 7E".ToHexBytes();
            JT808Package jT808_0X0100 = new JT808Package(bytes);
            jT808_0X0100.ReadBuffer(jT808GlobalConfigs);
        }
    }
}
