using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using GPS.PubSub.Abstractions;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_UnificationSend_Producer : JT808MsgIdProducerBase
    {
        private Producer<string, byte[]> producer;

        public JT808_UnificationSend_Producer()
        {
            producer = new Producer<string, byte[]>(Config, new StringSerializer(Encoding.UTF8), new ByteArraySerializer());
        }

        public JT808_UnificationSend_Producer(Dictionary<string, object> config) : base(config)
        {
            producer = new Producer<string, byte[]>(Config, new StringSerializer(Encoding.UTF8), new ByteArraySerializer());
        }

        public override void Dispose()
        {
            producer.Dispose();
        }

        public override string TopicName => PubSubConstants.UnificationSend;

        public override void ProduceAsync(string key,byte[] data)
        {
            producer.ProduceAsync(TopicName, key, data);
        }
    }
}
