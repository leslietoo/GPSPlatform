using JT808.MsgId0x0200Services.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JT808.Protocol.Extensions;
using GPS.PubSub.Abstractions;

namespace JT808.MsgId0x0200Services
{
    public  class ToWebSocketService: IHostedService
    {
        private readonly IConsumerFactory ConsumerFactory;

        private readonly ILogger<ToWebSocketService> logger;

        private readonly IHubContext<AlarmHub> _hubContext;

        public ToWebSocketService(ILoggerFactory loggerFactory,
            IHubContext<AlarmHub> hubContext,
            IConsumerFactory consumerFactory)
        {
            this._hubContext = hubContext;
            ConsumerFactory = consumerFactory;
            logger = loggerFactory.CreateLogger<ToWebSocketService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                ConsumerFactory
                        .Subscribe((ushort)JT808.Protocol.Enums.JT808MsgId.位置信息汇报)
                        .OnMessage((msg) =>
                        {
                            try
                            {
                                _hubContext.Clients.All.SendAsync("ReceiveMessage", $"Home page loaded at: {DateTime.Now}");
                                _hubContext.Clients.All.SendAsync("ReceiveMessage", msg.data.ToHexString());
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "Error");
                            }
                        });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error");
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stop ...");
            ConsumerFactory.Unsubscribe((ushort)JT808.Protocol.Enums.JT808MsgId.位置信息汇报);
            logger.LogInformation("Stop CompletedTask");
            return Task.CompletedTask;
        }
    }
}
