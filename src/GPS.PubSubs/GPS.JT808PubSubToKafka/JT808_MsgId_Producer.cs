using Confluent.Kafka;
using Confluent.Kafka.Admin;
using GPS.JT808PubSubToKafka.JT808Partitions;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_MsgId_Producer : IJT808Producer
    {
        public string TopicName => JT808PubSubConstants.JT808TopicName;

        private Producer<string, byte[]> producer;

        private IJT808ProducerPartitionFactory jT808ProducerPartitionFactory;

        private ConcurrentDictionary<string, TopicPartition> TopicPartitionCache = new ConcurrentDictionary<string, TopicPartition>();

        public JT808_MsgId_Producer(
            IOptions<ProducerConfig> producerConfigAccessor)
        {
            producer = new Producer<string, byte[]>(producerConfigAccessor.Value);
            this.jT808ProducerPartitionFactory = new JT808MsgIdProducerPartitionFactoryImpl();
            using (var adminClient = new AdminClient(producer.Handle))
            {
                try
                {
                    adminClient.CreateTopicsAsync(new TopicSpecification[] { new TopicSpecification { Name = TopicName, NumPartitions = 1, ReplicationFactor = 1 } }).Wait();
                }
                catch (AggregateException ex)
                {
                    //{Confluent.Kafka.Admin.CreateTopicsException: An error occurred creating topics: [jt808]: [Topic 'jt808' already exists.].}
                    if (ex.InnerException is Confluent.Kafka.Admin.CreateTopicsException exception)
                    {

                    }
                    else
                    {
                        throw ex.InnerException;
                    }
                }
                try
                {
                    //topic IncreaseTo 只增不减
                    adminClient.CreatePartitionsAsync(
                                    new List<PartitionsSpecification>
                                    {
                                        new PartitionsSpecification
                                        {
                                             IncreaseTo = 8,
                                             Topic=TopicName
                                        }
                                    }
                                ).Wait();
                }
                catch (AggregateException ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        public void Dispose()
        {
            producer.Dispose();
        }

        public void ProduceAsync(string msgId, string terminalNo, byte[] data)
        {
            TopicPartition topicPartition;
            if (!TopicPartitionCache.TryGetValue(terminalNo,out topicPartition))
            {
                topicPartition = new TopicPartition(TopicName,new Partition(jT808ProducerPartitionFactory.CreatePartition(TopicName, msgId, terminalNo)));
                TopicPartitionCache.TryAdd(terminalNo, topicPartition);
            }
            producer.ProduceAsync(topicPartition, new Message<string, byte[]>
            {
                Key = msgId,
                Value = data
            });
        }
    }
}
