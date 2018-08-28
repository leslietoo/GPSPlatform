using System.Collections.Generic;

namespace GPS.PubSub.Abstractions
{
    public interface IConsumerFactory
    {
        IDictionary<string, IConsumer> ConsumerDict { get; }
    }
}
