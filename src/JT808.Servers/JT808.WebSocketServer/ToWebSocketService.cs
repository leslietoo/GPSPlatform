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
using JT808.WebSocketServer.Hubs;

namespace JT808.WebSocketServer
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
                        .Subscribe(PubSubConstants.UnificationPushToWebSocket)
                        .OnMessage((msg) =>
                        {
                            try
                            {
                                _hubContext.Clients.All.SendAsync("ReceiveMessage", msg.Key, Encoding.UTF8.GetString(msg.data));
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
            ConsumerFactory.Unsubscribe(PubSubConstants.UnificationPushToWebSocket);
            logger.LogInformation("Stop CompletedTask");
            return Task.CompletedTask;
        }
    }
}
