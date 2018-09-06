using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using GPS.Dispatcher.Abstractions;
using GPS.JT808PubSubToKafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_DeviceMonitoringDispatcher_Producer : JT808MsgIdProducerBase
    {
        public override string TopicName => DispatcherConstants.DeviceMonitoringTopic;

        private Producer<string, byte[]> producer;

        public JT808_DeviceMonitoringDispatcher_Producer()
        {
            producer = new Producer<string, byte[]>(Config, new StringSerializer(Encoding.UTF8), new ByteArraySerializer());
        }

        public JT808_DeviceMonitoringDispatcher_Producer(Dictionary<string, object> config) : base(config)
        {
            producer = new Producer<string, byte[]>(Config, new StringSerializer(Encoding.UTF8), new ByteArraySerializer());
        }

        public override void ProduceAsync(string key, byte[] data)
        {
            producer.ProduceAsync(TopicName, key, data);
        }

        public override void Dispose()
        {
            producer.Dispose();
        }
    }
}
