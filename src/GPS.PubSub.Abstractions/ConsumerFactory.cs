using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS.PubSub.Abstractions
{
   public class ConsumerFactory: IConsumerFactory
    {
        public IDictionary<ushort, IConsumer> ConsumerDict { get; }

        public ConsumerFactory(params IConsumer[] consumers)
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
