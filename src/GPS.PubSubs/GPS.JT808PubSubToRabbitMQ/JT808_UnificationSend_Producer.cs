using EasyNetQ;
using GPS.JT808PubSubToRabbitMQ.JT808RabbitMQMessage;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808PubSubToRabbitMQ
{
    public class JT808_UnificationSend_Producer : JT808MsgIdProducerBase
    {
        private readonly IBus bus;

        public JT808_UnificationSend_Producer()
        {
            bus = RabbitHutch.CreateBus(ConnStr);
        }

        public JT808_UnificationSend_Producer(string connStr) : base(connStr)
        {
            bus = RabbitHutch.CreateBus(ConnStr);
        }

        public override string TopicName => "UnificationSend";

        public override void Dispose()
        {
            bus.Dispose();
        }

        public override void ProduceAsync(string key,byte[] data)
        {
            bus.PublishAsync(new JT808_UnificationSend_Message {Key=key,Data=data }, TopicName);
        }
    }
}
