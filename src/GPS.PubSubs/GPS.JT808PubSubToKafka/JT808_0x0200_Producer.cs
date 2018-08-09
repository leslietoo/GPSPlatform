using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Collections.Generic;

namespace GPS.JT808PubSubToKafka
{
    public sealed class JT808_0x0200_Producer : JT808MsgIdProducerBase
    {
        private Producer<Null, byte[]> producer;

        public JT808_0x0200_Producer()
        {
            producer = new Producer<Null, byte[]>(Config, null, new ByteArraySerializer());
        }

        public JT808_0x0200_Producer(Dictionary<string, object> config) : base(config)
        {
            producer = new Producer<Null, byte[]>(Config, null, new ByteArraySerializer());
        }

        public override void ProduceAsync(byte[] data)
        {
            producer.ProduceAsync(JT808MsgIdTopic, null, data);
        }

        public override ushort CategoryId  => (ushort)JT808.Protocol.Enums.JT808MsgId.位置信息汇报;


    }
}
