using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS.PubSub.Abstractions
{
    public class ProducerFactory: IProducerFactory
    {
        public IDictionary<ushort, IProducer> ProducerDict { get; }

        public ProducerFactory(params IProducer[] producers)
        {
            if (producers != null)
            {
                ProducerDict = producers.ToDictionary(key => key.CategoryId, value => value);
            }
            else
            {
                ProducerDict = new Dictionary<ushort, IProducer>();
            }
        }
    }
}
