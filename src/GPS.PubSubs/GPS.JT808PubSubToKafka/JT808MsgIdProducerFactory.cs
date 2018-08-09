using GPS.PubSub.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace GPS.JT808PubSubToKafka
{
    public class JT808MsgIdProducerFactory : IProducerFactory
    {
        public IDictionary<ushort, IProducer> ProducerDict { get ;}

        public JT808MsgIdProducerFactory(params IProducer[] producers)
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
