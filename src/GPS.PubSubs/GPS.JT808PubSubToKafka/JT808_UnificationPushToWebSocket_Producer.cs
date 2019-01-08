using Confluent.Kafka;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808PubSubToKafka
{
    /// <summary>
    /// 
    /// </summary>
    public class JT808_UnificationPushToWebSocket_Producer : IJT808Producer
    {
        public string TopicName => JT808PubSubConstants.UnificationPushToWebSocket;

        private Producer<string, byte[]> producer;

        public JT808_UnificationPushToWebSocket_Producer(IOptions<ProducerConfig> producerConfigAccessor)
        {
            producer = new Producer<string, byte[]>(producerConfigAccessor.Value);
        }

        public void Dispose()
        {
            producer.Dispose();
        }

        public void ProduceAsync(string msgId, string terminalNo, byte[] data)
        {
            producer.ProduceAsync(TopicName, new Message<string, byte[]>
            {
                Key = msgId,
                Value = data
            });
        }
    }
}
