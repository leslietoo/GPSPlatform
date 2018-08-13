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

        public override ushort CategoryId => (ushort)JT808MsgId.自定义统一下发消息;

        public override void Dispose()
        {
            bus.Dispose();
        }

        public override void ProduceAsync(string key,byte[] data)
        {
            bus.PublishAsync(new JT808_UnificationSend_Message {Key=key,Data=data }, $"{JT808MsgIdTopic}.{key}");
        }
    }
}
