
using GPS.PubSub.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GPS.JT808PubSubToKafka.Test
{
    public class JT808HashAlgorithmTest
    {
        [Fact]
        public void Test1()
        {
            var key1Byte1 = JT808HashAlgorithm.ComputeMd5("1234567890");
            var key1Byte2 = JT808HashAlgorithm.ComputeMd5("4534567896");
            var h1 = JT808HashAlgorithm.Hash(key1Byte1, 1) % 2;
            var h2 = JT808HashAlgorithm.Hash(key1Byte2, 1) % 2;
        }


        [Fact]
        public void Test2()
        {
            int n = 20000;
            List<int> p = new List<int>();
            for (var i = 0; i <= n; i++)
            {
                var key1Byte = JT808HashAlgorithm.ComputeMd5(Guid.NewGuid().ToString("N").Substring(0, 12));
                var h = (int)(JT808HashAlgorithm.Hash(key1Byte, 1) % Math.Pow(2,2));
                p.Add(h);
            }

            var result = p.GroupBy(g => g).Select(s => new
            {
                Key = s.Key,
                Count = s.Count(),
                Percent = s.Count() / (n * 1.0) * 100
            }).ToList();
        }

        class PsTest3
        {
            public string Key { get; set; }
            public int P { get; set; }
        }

        [Fact]
        public void Test3()
        {
            int n = 100;
            List<PsTest3> ps = new List<PsTest3>();
            var key1Byte1 = JT808HashAlgorithm.ComputeMd5("1234567890");
            var key1Byte2 = JT808HashAlgorithm.ComputeMd5("4534567896");
            var key1Byte3 = JT808HashAlgorithm.ComputeMd5("a534567897");
            var key1Byte4 = JT808HashAlgorithm.ComputeMd5("a534567812");
            var key1Byte5 = JT808HashAlgorithm.ComputeMd5("a534567842");

            var h1 = JT808HashAlgorithm.Hash(key1Byte1) % 2;
            var h2 = JT808HashAlgorithm.Hash(key1Byte2) % 2;
            var h3 = JT808HashAlgorithm.Hash(key1Byte3) % 2;
            var h4 = JT808HashAlgorithm.Hash(key1Byte4) % 2;
            var h5 = JT808HashAlgorithm.Hash(key1Byte5) % 2;

            var h14 = JT808HashAlgorithm.Hash(key1Byte1) % 4;
            var h24 = JT808HashAlgorithm.Hash(key1Byte2) % 4;
            var h34 = JT808HashAlgorithm.Hash(key1Byte3) % 4;
            var h44 = JT808HashAlgorithm.Hash(key1Byte4) % 4;
            var h54 = JT808HashAlgorithm.Hash(key1Byte5) % 4;

            var h18 = JT808HashAlgorithm.Hash(key1Byte1) % 8;
            var h28 = JT808HashAlgorithm.Hash(key1Byte2) % 8;
            var h38 = JT808HashAlgorithm.Hash(key1Byte3) % 8;
            var h48 = JT808HashAlgorithm.Hash(key1Byte4) % 8;
            var h58 = JT808HashAlgorithm.Hash(key1Byte5) % 8;


        }

    }
}
