using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol;
using Protocol.Common.Extensions;

namespace JT808.Protocol.Test
{
    public class JT808PackageTest
    {
        [Fact]
        public void Test()
        {
            Memory<byte> memory = new byte[5];
            memory.Span.WriteLittle(5, 0, 2);
        }

        [Fact]
        public void Test1()
        {
    
        }
    }
}
