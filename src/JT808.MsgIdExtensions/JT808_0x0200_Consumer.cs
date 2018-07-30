using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.MsgIdExtensions
{
    public class JT808_0x0200_Consumer : JT808MsgIdConsumerBase<Ignore, byte[]>
    {
        public JT808_0x0200_Consumer()
        {
            MsgIdConsumer = new Consumer<Ignore, byte[]>(Config, null, new ByteArrayDeserializer());
        }

        public JT808_0x0200_Consumer(Dictionary<string, object> config) : base(config)
        {
            MsgIdConsumer=new Consumer<Ignore, byte[]>(Config, null, new ByteArrayDeserializer());
        }

        public override JT808MsgId JT808MsgId => JT808MsgId.位置信息汇报;

        public override Consumer<Ignore, byte[]> MsgIdConsumer { get; set; }

    }
}
