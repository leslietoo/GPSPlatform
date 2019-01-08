using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace GPS.JT808PubSubToKafka.Test
{
    public  class JT808_MsgId_ProducerTest:TestBase
    {
        [Fact]
        public void Test1()
        {
            var jT808_MsgId_Producer = ServiceProvider.GetRequiredService<JT808_MsgId_Producer>();

            jT808_MsgId_Producer.ProduceAsync("512", "1234567890", new byte[] { 0, 1, 2, 3 });
            jT808_MsgId_Producer.ProduceAsync("512",  "4534567896", new byte[] { 0, 1, 2, 3 , 4 });
            jT808_MsgId_Producer.ProduceAsync("1024", "123456", new byte[] { 0, 1, 2, 3, 4 });
            jT808_MsgId_Producer.ProduceAsync("1024", "1234567", new byte[] { 0, 1, 2, 3, 4 });
        }

        [Fact]
        public void Test2()
        {
            var jT808_MsgId_Consumer = ServiceProvider.GetRequiredService<JT808_MsgId_Consumer>();
 
            jT808_MsgId_Consumer.OnMessage("512", (msg) =>
            {

            });
            jT808_MsgId_Consumer.Subscribe();
            Thread.Sleep(10000);

        }
    }
}
