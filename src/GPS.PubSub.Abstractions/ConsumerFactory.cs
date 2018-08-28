using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS.PubSub.Abstractions
{
   public class ConsumerFactory: IConsumerFactory
    {
        public IDictionary<string, IConsumer> ConsumerDict { get; }

        public ConsumerFactory(params IConsumer[] consumers)
        {
            if (consumers != null)
            {
                ConsumerDict = consumers.ToDictionary(key => key.TopicName, value => value);
            }
            else
            {
                ConsumerDict = new Dictionary<string, IConsumer>();
            }
        }
    }
}
