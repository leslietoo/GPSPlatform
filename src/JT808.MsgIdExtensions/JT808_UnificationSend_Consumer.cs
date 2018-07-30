using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.MsgIdExtensions
{
    public class JT808_UnificationSend_Consumer : JT808MsgIdConsumerBase<string, byte[]>
    {
        public JT808_UnificationSend_Consumer()
        {
            MsgIdConsumer= new Consumer<string, byte[]>(Config, new StringDeserializer(Encoding.UTF8), new ByteArrayDeserializer());
        }

        public JT808_UnificationSend_Consumer(Dictionary<string, object> config) : base(config)
        {
            MsgIdConsumer = new Consumer<string, byte[]>(Config, new StringDeserializer(Encoding.UTF8), new ByteArrayDeserializer());
        }

        public override Consumer<string, byte[]> MsgIdConsumer { get; set; }


        public override JT808MsgId JT808MsgId => JT808MsgId.自定义统一下发消息;
    }
}
