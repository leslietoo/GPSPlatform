using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_0x0200_Consumer : JT808MsgIdConsumerBase
    {
        private Consumer<Null, byte[]> consumer;

        public JT808_0x0200_Consumer()
        {
            consumer = new Consumer<Null, byte[]>(Config, null, new ByteArrayDeserializer());
        }

        public JT808_0x0200_Consumer(Dictionary<string, object> config) : base(config)
        {
            consumer = new Consumer<Null, byte[]>(Config, null, new ByteArrayDeserializer());
        }

        public override ushort CategoryId => (ushort)JT808.Protocol.Enums.JT808MsgId.位置信息汇报;

        public override event EventHandler<object> OnMessage;

        public override event EventHandler<object> OnError;

        public override event EventHandler<object> OnConsumeError;

        public override void Dispose()
        {
            consumer.Dispose();
        }

        public override void Subscribe()
        {
            
            consumer.Subscribe(JT808MsgIdTopic);
        }

        public override void Unsubscribe()
        {
            consumer.Unsubscribe();
        }
    }
}
