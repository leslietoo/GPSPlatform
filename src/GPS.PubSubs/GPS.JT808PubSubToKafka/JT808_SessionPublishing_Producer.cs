using Confluent.Kafka;
using GPS.PubSub.Abstractions;
using JT808.DotNetty.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_SessionPublishing_Producer : 
        IJT808Producer, 
        IJT808SessionPublishing
    {
        public string TopicName => throw new NotImplementedException();

        private Producer<Null, string> producer;

        public JT808_SessionPublishing_Producer(IOptions<ProducerConfig> producerConfigAccessor)
        {
            producer = new Producer<Null, string>(producerConfigAccessor.Value);
        }

        public void Dispose()
        {
            producer.Dispose();
        }


        public Task PublishAsync(string topicName, string value)
        {
            producer.ProduceAsync(topicName, new Message<Null, string>
            {
                Value = value
            });
            return Task.CompletedTask;
        }

        public void ProduceAsync(string msgId, string terminalNo, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
