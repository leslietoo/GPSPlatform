using EasyNetQ;
using GPS.JT808PubSubToRabbitMQ.JT808RabbitMQMessage;
using GPS.PubSub.Abstractions;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToRabbitMQ
{
    public class JT808_UnificationSend_Consumer : JT808MsgIdConsumerBase
    {
        protected override ILogger Logger { get; }

        private readonly IBus bus;

        private ISubscriptionResult subscriptionResult;

        public JT808_UnificationSend_Consumer(ILoggerFactory loggerFactory):base(loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_UnificationSend_Consumer>();
            bus = RabbitHutch.CreateBus(ConnStr);
        }

        public JT808_UnificationSend_Consumer(string connStr, ILoggerFactory loggerFactory) : base(connStr, loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_UnificationSend_Consumer>();
            bus = RabbitHutch.CreateBus(ConnStr);
        }

        public override string TopicName => PubSubConstants.UnificationSend;

        public override void OnMessage(Action<(string Key, byte[] data)> callback)
        {
            subscriptionResult=bus.Subscribe<JT808_UnificationSend_Message>(TopicName, (msg)=> 
            {
                callback((msg.Key, msg.Data));
            });
        }

        public override void Subscribe()
        {

        }

        public override void Unsubscribe()
        {
            subscriptionResult.ConsumerCancellation.Dispose();
            subscriptionResult.Dispose();
            bus.Dispose();
        }
    }
}
