using System;
using System.Collections.Generic;

namespace GPS.PubSub.Abstractions
{
    public interface IProducerFactory
    {
        IDictionary<ushort, IProducer> ProducerDict { get; }
    }
}
