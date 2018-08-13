using GPS.PubSub.Abstractions;

namespace GPS.JT808PubSubToRabbitMQ
{
    public abstract class JT808MsgIdProducerBase: JT808MsgIdBase,IProducer
    {
        public JT808MsgIdProducerBase()
        {
        }

        public JT808MsgIdProducerBase(string conn) : base(conn)
        {
        }

        public abstract void Dispose();
        public abstract void ProduceAsync(string key, byte[] data);
    }
}
