using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JT808.MsgIdExtensions
{
    public class JT808_0x0200_Producer : JT808MsgIdProducerBase<Null, byte[]>
    {
        public JT808_0x0200_Producer()
        {
            MsgIdProducer = new Producer<Null, byte[]>(Config, null, new ByteArraySerializer());
        }

        public JT808_0x0200_Producer(Dictionary<string, object> config) : base(config)
        {
            MsgIdProducer=new Producer<Null, byte[]>(Config, null, new ByteArraySerializer());
        }

        public void ProduceAsync(byte[] val)
        {
            MsgIdProducer.ProduceAsync(JT808MsgIdTopic, null, val);
        }

        public override JT808MsgId JT808MsgId => JT808MsgId.位置信息汇报;

        public override Producer<Null, byte[]> MsgIdProducer { get; set; }
    }
}
