using Confluent.Kafka;
using GPS.PubSub.Abstractions;
using JT808.DotNetty.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_SessionPublishing_Consumer : IJT808Consumer
    {
        public string TopicName => throw new NotImplementedException();

        public CancellationTokenSource Cts => new CancellationTokenSource();

        private readonly ILogger<JT808_SessionPublishing_Consumer> logger;

        private Consumer<Null, string> consumer;

        public JT808_SessionPublishing_Consumer(IOptions<ConsumerConfig> consumerConfigAccessor, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<JT808_SessionPublishing_Consumer>();
            consumer = new Consumer<Null, string>(consumerConfigAccessor.Value);
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
                        if (logger.IsEnabled(LogLevel.Debug))
                        {
                            logger.LogDebug($"Topic: {data.Topic} Partition: {data.Partition} Offset: {data.Offset} Data:{string.Join("", data.Value)} TopicPartitionOffset:{data.TopicPartitionOffset}");
                        }
                        callback((null,Encoding.UTF8.GetBytes(data.Value)));
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
