using EasyNetQ;
using EasyNetQ.Topology;
using GPS.PubSub.Abstractions;
using JT808.Protocol.Extensions;
using System.Collections.Generic;

namespace GPS.JT808PubSubToRabbitMQ
{
    public sealed class JT808_0x0200_Producer : JT808MsgIdProducerBase
    {
        private readonly IBus bus;

        public JT808_0x0200_Producer()
        {
            bus=RabbitHutch.CreateBus(ConnStr);
           // bus.Advanced.ExchangeDeclare($"{JT808MsgIdTopic}-exchange", ExchangeType.Fanout, true);
        }

        public JT808_0x0200_Producer(string connStr) : base(connStr)
        {
            bus = RabbitHutch.CreateBus(ConnStr);
           /// bus.Advanced.ExchangeDeclare($"{JT808MsgIdTopic}-exchange", ExchangeType.Fanout, true);
        }

        public override void ProduceAsync(string key,byte[] data)
        {
            var exchange = bus.Advanced.ExchangeDeclare(TopicName, ExchangeType.Fanout);
            bus.Advanced.Publish(exchange, "", false, new Message<byte[]>(data));
        }

        public override void Dispose()
        {
            bus.Dispose();
        }

        public override string TopicName => JT808.Protocol.Enums.JT808MsgId.位置信息汇报.ToValueString();



    }
}
