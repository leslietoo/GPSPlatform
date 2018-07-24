using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.MsgIdExtensions
{
    public class JT808_0x0200_Consumer : JT808MsgIdConsumerBase<Ignore, string>
    {
        public JT808_0x0200_Consumer()
        {
            MsgIdConsumer = new Consumer<Ignore, string>(Config, null, new StringDeserializer(Encoding.UTF8));
        }

        public JT808_0x0200_Consumer(Dictionary<string, object> config) : base(config)
        {
            MsgIdConsumer = new Consumer<Ignore, string>(Config, null, new StringDeserializer(Encoding.UTF8));
        }

        public override JT808MsgId JT808MsgId => JT808MsgId.位置信息汇报;

        public override Consumer<Ignore, string> MsgIdConsumer { get; set; }

    }
}
