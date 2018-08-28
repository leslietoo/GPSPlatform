using System;
using System.Collections.Generic;

namespace GPS.PubSub.Abstractions
{
    public interface IProducerFactory
    {
        IDictionary<string, IProducer> ProducerDict { get; }
    }
}
