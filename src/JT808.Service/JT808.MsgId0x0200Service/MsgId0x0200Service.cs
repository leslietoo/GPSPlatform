using Confluent.Kafka;
using JT808.MsgIdExtensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JT808.MsgId0x0200Service
{
    public class MsgId0x0200Service : IHostedService
    {
        private readonly JT808_0x0200_Consumer jT808_0X0200_Consumer;

        private readonly ILogger<MsgId0x0200Service> logger;

        public MsgId0x0200Service(ILoggerFactory loggerFactory, JT808_0x0200_Consumer jT808_0X0200_Consumer)
        {
            this.jT808_0X0200_Consumer = jT808_0X0200_Consumer;
            logger = loggerFactory.CreateLogger<MsgId0x0200Service>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            jT808_0X0200_Consumer.MsgIdConsumer.OnMessage += (_, msg) =>
            {
                // todo: 处理定位数据
                logger.LogDebug($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
            };
            jT808_0X0200_Consumer.MsgIdConsumer.OnError += (_, error) =>
            {
                logger.LogError($"Error: {error}");
            };
            jT808_0X0200_Consumer.MsgIdConsumer.OnConsumeError += (_, msg) =>
            {
                logger.LogError($"Error consuming from topic/partition/offset {msg.Topic}/{msg.Partition}/{msg.Offset}: {msg.Error}");
            };
            jT808_0X0200_Consumer.MsgIdConsumer.Subscribe(((ushort)jT808_0X0200_Consumer.JT808MsgId).ToString());
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    jT808_0X0200_Consumer.MsgIdConsumer.Poll(TimeSpan.FromMilliseconds(100));
                }
            }, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stop ...");
            jT808_0X0200_Consumer.MsgIdConsumer.Unsubscribe();
            jT808_0X0200_Consumer.MsgIdConsumer.Dispose();
            logger.LogInformation("Stop CompletedTask");
            return Task.CompletedTask;
        }
    }
}
