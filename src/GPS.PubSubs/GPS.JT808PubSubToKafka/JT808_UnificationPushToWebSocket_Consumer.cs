using Confluent.Kafka;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_UnificationPushToWebSocket_Consumer : IJT808Consumer
    {
        public CancellationTokenSource Cts => new CancellationTokenSource();

        public string TopicName => JT808PubSubConstants.JT808TopicName;

        private readonly ILogger<JT808_UnificationPushToWebSocket_Consumer> logger;

        private Consumer<string, byte[]> consumer;

        public JT808_UnificationPushToWebSocket_Consumer(IOptions<ConsumerConfig> consumerConfigAccessor, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<JT808_UnificationPushToWebSocket_Consumer>();
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
                        var data = consumer.Consume(Cts.Token);
                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug($"Topic: {data.Topic} Key: {data.Key} Partition: {data.Partition} Offset: {data.Offset} Data:{string.Join("", data.Value)} TopicPartitionOffset:{data.TopicPartitionOffset}");
                        }
                        callback((data.Key, data.Value));
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
