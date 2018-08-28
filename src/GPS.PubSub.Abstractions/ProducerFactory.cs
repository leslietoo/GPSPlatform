using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS.PubSub.Abstractions
{
    public class ProducerFactory: IProducerFactory
    {
        public IDictionary<string, IProducer> ProducerDict { get; }

        public ProducerFactory(params IProducer[] producers)
        {
            if (producers != null)
            {
                ProducerDict = producers.ToDictionary(key => key.TopicName, value => value);
            }
            else
            {
                ProducerDict = new Dictionary<string, IProducer>();
            }
        }
    }
}
