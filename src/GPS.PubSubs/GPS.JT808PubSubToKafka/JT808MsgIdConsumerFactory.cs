using GPS.PubSub.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace GPS.JT808PubSubToKafka
{
    public class JT808MsgIdConsumerFactory : IConsumerFactory
    {
        public IDictionary<ushort, IConsumer> ConsumerDict { get ;}

        public JT808MsgIdConsumerFactory(params IConsumer[] consumers)
        {
            if (consumers != null)
            {
                ConsumerDict = consumers.ToDictionary(key => key.CategoryId, value => value);
            }
            else
            {
                ConsumerDict = new Dictionary<ushort, IConsumer>();
            }
        }
    }
}
