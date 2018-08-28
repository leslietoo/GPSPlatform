using EasyNetQ;
using EasyNetQ.Topology;
using JT808.Protocol.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace GPS.JT808PubSubToRabbitMQ
{
    public class JT808_0x0200_Consumer : JT808MsgIdConsumerBase
    {
        protected override ILogger Logger { get; }

        private readonly IBus bus;

        private IDisposable consumeDisposable;

        public JT808_0x0200_Consumer(ILoggerFactory loggerFactory) :base(loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_0x0200_Consumer>();
            bus=RabbitHutch.CreateBus(ConnStr);
        }

        public JT808_0x0200_Consumer(string connStr, ILoggerFactory loggerFactory) : base(connStr, loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_0x0200_Consumer>();
            bus = RabbitHutch.CreateBus(ConnStr);
        }

        public override string TopicName => JT808.Protocol.Enums.JT808MsgId.位置信息汇报.ToValueString();

        public override void OnMessage(Action<(string Key, byte[] data)> callback)
        {
            var exchange = bus.Advanced.ExchangeDeclare(TopicName, ExchangeType.Fanout);
            IQueue queue= bus.Advanced.QueueDeclare(TopicName + ExchangeType.Fanout);
            bus.Advanced.Bind(exchange, queue, "");
            consumeDisposable=bus.Advanced.Consume<byte[]>(queue,(msg, mri)=> 
            {
                callback((TopicName + ExchangeType.Fanout, msg.Body));
            });
        }

        public override void Subscribe()
        {

        }

        public override void Unsubscribe()
        {
            consumeDisposable.Dispose();
            bus.Dispose();
        }
    }
}
