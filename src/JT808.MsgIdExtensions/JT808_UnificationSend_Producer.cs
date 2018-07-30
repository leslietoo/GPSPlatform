using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.MsgIdExtensions
{
    public class JT808_UnificationSend_Producer : JT808MsgIdProducerBase<string, byte[]>
    {
        public JT808_UnificationSend_Producer()
        {
            MsgIdProducer= new Producer<string, byte[]>(Config, new StringSerializer(Encoding.UTF8), new ByteArraySerializer());
        }

        public JT808_UnificationSend_Producer(Dictionary<string, object> config) : base(config)
        {
            MsgIdProducer = new Producer<string, byte[]>(Config, new StringSerializer(Encoding.UTF8), new ByteArraySerializer());
        }

        public override Producer<string, byte[]> MsgIdProducer { get; set; }

        public override JT808MsgId JT808MsgId => JT808MsgId.自定义统一下发消息;
    }
}
