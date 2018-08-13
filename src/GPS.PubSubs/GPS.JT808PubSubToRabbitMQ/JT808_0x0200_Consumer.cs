using EasyNetQ;
using EasyNetQ.Topology;
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

        public override ushort CategoryId => (ushort)JT808.Protocol.Enums.JT808MsgId.位置信息汇报;

        public override void OnMessage(Action<(string Key, byte[] data)> callback)
        {
            var exchange = bus.Advanced.ExchangeDeclare(JT808MsgIdTopic, ExchangeType.Fanout);
            string queueName = JT808MsgIdTopic +"_" + Guid.NewGuid().ToString("N").Substring(0,6);
            IQueue queue= bus.Advanced.QueueDeclare(queueName);
            bus.Advanced.Bind(exchange, queue, "anything");
            consumeDisposable=bus.Advanced.Consume<byte[]>(queue,(msg, mri)=> 
            {
                callback(("", msg.Body));
            });
            //var exchange1 = bus.Advanced.ExchangeDeclare(JT808MsgIdTopic, ExchangeType.Fanout);
            //IQueue queue1 = bus.Advanced.QueueDeclare(JT808MsgIdTopic+"2");
            //bus.Advanced.Bind(exchange, queue1, JT808MsgIdTopic);
            //bus.Advanced.Consume<byte[]>(queue1, (msg, mri) =>
            //{
            //    callback(("", msg.Body));
            //});
            //subscriptionResult =bus.Subscribe<byte[]>(JT808MsgIdTopic, msg => 
            //{
            //    callback(("", msg));
            //},config=> { config.WithTopic($"{JT808MsgIdTopic}.{new Random(10000).Next(int.MaxValue).ToString()}"); });
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
