using Confluent.Kafka;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_MsgId_Consumer : IJT808Consumer
    {
        public CancellationTokenSource Cts => new CancellationTokenSource();

        public string TopicName => JT808PubSubConstants.JT808TopicName;

        private readonly ILogger<JT808_MsgId_Consumer> logger;

        private Consumer<string, byte[]> consumer;

        public JT808_MsgId_Consumer(
            IOptions<ConsumerConfig> consumerConfigAccessor, 
            ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<JT808_MsgId_Consumer>();
            consumer = new Consumer<string, byte[]>(consumerConfigAccessor.Value);
            consumer.OnError += (_, e) =>
            {
                logger.LogError(e.Reason);
            };
        }

        public void OnMessage(string msgId, Action<(string MsgId, byte[] data)> callback)
        {
            Task.Run(() =>
            {
                while (!Cts.IsCancellationRequested)
                {
                    try
                    {
                        //如果不指定分区，根据kafka的机制会从多个分区中拉取数据
                        //如果指定分区，根据kafka的机制会从相应的分区中拉取数据
                        //consumer.Assign(new TopicPartition(TopicName,new Partition(0)));
                        var data = consumer.Consume(Cts.Token);
                        if(logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug($"Topic: {data.Topic} Key: {data.Key} Partition: {data.Partition} Offset: {data.Offset} Data:{string.Join("", data.Value)} TopicPartitionOffset:{data.TopicPartitionOffset}");
                        }
                        callback((data.Key, data.Value));
                        //if (data.Key== msgId)
                        //{
                        //    callback((data.Key, data.Value));
                        //}
                    }
                    catch (ConsumeException ex)
                    {
                        logger.LogError(ex, TopicName);
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, TopicName);
                        Thread.Sleep(1000);
                    }
                }
            }, Cts.Token);
        }

        public void Subscribe()
        {
            consumer.Subscribe(TopicName);
        }

        public void Unsubscribe()
        {
            consumer.Unsubscribe();
        }

        public void Dispose()
        {
            consumer.Close();
            consumer.Dispose();
        }
    }
}
