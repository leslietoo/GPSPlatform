using Confluent.Kafka;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Options;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_MsgId_Producer : IJT808Producer
    {
        public string TopicName => JT808PubSubConstants.JT808TopicName;

        private Producer<string, byte[]> producer;

        public JT808_MsgId_Producer(IOptions<ProducerConfig> producerConfigAccessor)
        {
            producer = new Producer<string, byte[]>(producerConfigAccessor.Value);
        }

        public void Dispose()
        {
            producer.Dispose();
        }

        public void ProduceAsync(string key, byte[] data)
        {
            producer.ProduceAsync(TopicName, new Message<string, byte[]>
            {
                Key= key,
                Value= data
            });
        }
    }
}
