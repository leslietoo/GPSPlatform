using System.Collections.Generic;

namespace GPS.PubSub.Abstractions
{
    public interface IConsumerFactory
    {
        IDictionary<ushort, IConsumer> ConsumerDict { get; }
    }
}
