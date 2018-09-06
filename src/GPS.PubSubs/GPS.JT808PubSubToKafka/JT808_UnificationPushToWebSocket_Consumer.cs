using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using GPS.Dispatcher.Abstractions;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_UnificationPushToWebSocket_Consumer : JT808MsgIdConsumerBase
    {
        public override string TopicName => PubSubConstants.UnificationPushToWebSocket;

        protected override ILogger Logger { get; }

        private Consumer<string, byte[]> consumer;

        public JT808_UnificationPushToWebSocket_Consumer(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_UnificationPushToWebSocket_Consumer>();
            consumer = new Consumer<string, byte[]>(Config, new StringDeserializer(Encoding.UTF8), new ByteArrayDeserializer());
            RegisterEvent();
        }

        public JT808_UnificationPushToWebSocket_Consumer(Dictionary<string, object> config, ILoggerFactory loggerFactory) : base(config, loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_UnificationPushToWebSocket_Consumer>();
            consumer = new Consumer<string, byte[]>(Config, new StringDeserializer(Encoding.UTF8), new ByteArrayDeserializer());
            RegisterEvent();
        }

        public override void OnMessage(Action<(string Key, byte[] data)> callback)
        {
            consumer.OnMessage += (_, msg) =>
            {
                callback((msg.Key, msg.Value));
            };
        }

        public override void Subscribe()
        {
            Task.Run(() =>
            {
                while (!Cts.IsCancellationRequested)
                {
                    try
                    {
                        consumer.Poll(TimeSpan.FromMilliseconds(100));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(Thread.CurrentThread.Name, ex);
                        Thread.Sleep(5000);
                    }
                }
            }, Cts.Token);
            consumer.Subscribe(TopicName);
        }

        public override void Unsubscribe()
        {
            Cts.Cancel();
            consumer.Unsubscribe();
        }

        protected override void RegisterEvent()
        {
            consumer.OnError += (_, error) =>
            {
                Logger.LogError($"Error: {error}");
            };
            consumer.OnConsumeError += (_, msg) =>
            {
                Logger.LogError($"Error consuming from topic/partition/offset {msg.Topic}/{msg.Partition}/{msg.Offset}: {msg.Error}");
            };
        }
    }
}
