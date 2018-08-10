using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_UnificationSend_Consumer : JT808MsgIdConsumerBase
    {
        private Consumer<string, byte[]> consumer;

        public JT808_UnificationSend_Consumer(ILoggerFactory loggerFactory):base(loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_UnificationSend_Consumer>();
            consumer = new Consumer<string, byte[]>(Config, new StringDeserializer(Encoding.UTF8), new ByteArrayDeserializer());
            RegisterEvent();
        }

        public JT808_UnificationSend_Consumer(Dictionary<string, object> config, ILoggerFactory loggerFactory) : base(config, loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_UnificationSend_Consumer>();
            consumer = new Consumer<string, byte[]>(Config, new StringDeserializer(Encoding.UTF8), new ByteArrayDeserializer());
            RegisterEvent();
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

        public override ushort CategoryId =>(ushort) JT808MsgId.自定义统一下发消息;

        protected override ILogger Logger { get; }

        public override void OnMessage(Action<(string Key, byte[] data)> callback)
        {
            consumer.OnMessage += (_, msg) =>
            {
                // todo: 处理下发数据
                Logger.LogDebug($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value.ToHexString()}");
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
            consumer.Subscribe(JT808MsgIdTopic);
        }

        public override void Unsubscribe()
        {
            Cts.Cancel();
            consumer.Unsubscribe();
        }
    }
}
