using JT808.MsgId0x0200Services.Hubs;
using JT808.MsgIdExtensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JT808.Protocol.Extensions;

namespace JT808.MsgId0x0200Services
{
    public  class ToWebSocketService: IHostedService
    {
        private readonly JT808_0x0200_Consumer jT808_0X0200_Consumer;

        private readonly ILogger<ToWebSocketService> logger;

        private readonly IHubContext<AlarmHub> _hubContext;

        public ToWebSocketService(ILoggerFactory loggerFactory,
            IHubContext<AlarmHub> hubContext,
            JT808_0x0200_Consumer jT808_0X0200_Consumer)
        {
            this._hubContext = hubContext;
            this.jT808_0X0200_Consumer = jT808_0X0200_Consumer;
            logger = loggerFactory.CreateLogger<ToWebSocketService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            jT808_0X0200_Consumer.MsgIdConsumer.OnMessage += (_, msg) =>
            {
                // todo: 处理定位数据
                logger.LogDebug($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value.ToHexString()}");
                try
                {
                    _hubContext.Clients.All.SendAsync("ReceiveMessage", $"Home page loaded at: {DateTime.Now}");
                    _hubContext.Clients.All.SendAsync("ReceiveMessage", msg.Value.ToHexString());
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error");
                }
            };
            jT808_0X0200_Consumer.MsgIdConsumer.OnError += (_, error) =>
            {
                logger.LogError($"Error: {error}");
            };
            jT808_0X0200_Consumer.MsgIdConsumer.OnConsumeError += (_, msg) =>
            {
                logger.LogError($"Error consuming from topic/partition/offset {msg.Topic}/{msg.Partition}/{msg.Offset}: {msg.Error}");
            };
            jT808_0X0200_Consumer.MsgIdConsumer.Subscribe(jT808_0X0200_Consumer.JT808MsgIdTopic);
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
