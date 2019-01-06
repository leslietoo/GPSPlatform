using Confluent.Kafka;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
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

        public JT808_MsgId_Consumer(IOptions<ConsumerConfig> consumerConfigAccessor, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<JT808_MsgId_Consumer>();
            consumer = new Consumer<string, byte[]>(consumerConfigAccessor.Value);
            consumer.OnError += (_, e) =>
            {
                logger.LogError(e.Reason);
            };
        }

        public void OnMessage(string key, Action<(string Key, byte[] data)> callback)
        {
            Task.Run(() =>
            {
                while (!Cts.IsCancellationRequested)
                {
                    try
                    {
                        var data = consumer.Consume(Cts.Token);
                        if(logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug($"Topic: {data.Topic} Partition: {data.Partition} Offset: {data.Offset} Data:{string.Join("",data.Value)} TopicPartitionOffset:{data.TopicPartitionOffset}");
                        }
                        if(key== data.Key)
                        {
                            callback((data.Key, data.Value));
                        }
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
