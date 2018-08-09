using GPS.PubSub.Abstractions;
using System.Collections.Generic;

namespace GPS.JT808PubSubToKafka
{
    public abstract class JT808MsgIdProducerBase: JT808MsgIdBase,IProducer
    {
        public JT808MsgIdProducerBase()
        {
        }

        public JT808MsgIdProducerBase(Dictionary<string, object> config) : base(config)
        {
        }

        public abstract void ProduceAsync(byte[] data);
    }
}
