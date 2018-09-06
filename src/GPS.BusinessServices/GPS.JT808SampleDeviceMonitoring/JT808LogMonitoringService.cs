using GPS.Dispatcher.Abstractions;
using GPS.JT808SampleDeviceMonitoring.Configs;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JT808.Protocol.Extensions;
using System.IO;
using Newtonsoft.Json.Linq;

namespace GPS.JT808SampleDeviceMonitoring
{
    public class JT808LogMonitoringService : IHostedService
    {
        private readonly IConsumerFactory ConsumerFactory;

        private readonly ILogger<JT808LogMonitoringService> logger;

        private readonly ILogger LogMonitoringLogger;

        private readonly IOptionsMonitor<LogMonitioringOptions> optionsMonitor;

        public JT808LogMonitoringService(
            IConsumerFactory consumerFactory,
            IOptionsMonitor<LogMonitioringOptions> optionsMonitor,
            ILoggerFactory loggerFactory
          )
        {
            ConsumerFactory = consumerFactory;
            this.optionsMonitor = optionsMonitor;
            logger = loggerFactory.CreateLogger<JT808LogMonitoringService>();
            LogMonitoringLogger = loggerFactory.CreateLogger("LogMonitoringLogger");
        }

        private DateTime? CurrentTime;


        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                Task.Run(() => {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        if (CurrentTime.HasValue)
                        {
                            if (CurrentTime.Value < DateTime.Now)
                            {
                                CurrentTime = null;
                                optionsMonitor.CurrentValue.MonitoringStatus = false;
                                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogMonitioringOptions.json");
                                if (!File.Exists(path))
                                {
                                    File.Create(path).Dispose();
                                    using (StreamWriter sw = new StreamWriter(path))
                                    {
                                        JObject jObject = new JObject();
                                        jObject.Add("LogMonitioringOptions", JToken.FromObject(optionsMonitor.CurrentValue));
                                        sw.WriteLine(jObject.ToString(Formatting.Indented));
                                    }
                                }
                                else
                                {
                                    using (StreamWriter sw = new StreamWriter(path))
                                    {
                                        JObject jObject = new JObject();
                                        jObject.Add("LogMonitioringOptions",  JToken.FromObject(optionsMonitor.CurrentValue));
                                        sw.WriteLine(jObject.ToString(Formatting.Indented));
                                    }
                                }
                            }
                            else
                            {
#if DEBUG
                                Thread.Sleep(10000);
#else
                                Thread.Sleep(36000);
#endif
                            }
                        }
                        else
                        {
#if DEBUG
                            Thread.Sleep(10000);
#else
                            Thread.Sleep(36000);
#endif
                        }
                    }
                }, cancellationToken);

                ConsumerFactory
                    .Subscribe(DispatcherConstants.DeviceMonitoringTopic)
                    .OnMessage((msg) =>
                    {
                        try
                        {
                            // 是否需要监控
                            if (optionsMonitor.CurrentValue.MonitoringStatus)
                            {
                                // 是不是第一次进来
                                if (!CurrentTime.HasValue)
                                {//监控多长时间
#if DEBUG
                                    CurrentTime = DateTime.Now.AddSeconds(optionsMonitor.CurrentValue.MonitoringTime);
#else
                                    CurrentTime = DateTime.Now.AddHours(optionsMonitor.CurrentValue.MonitoringTime);
#endif
                                }
                                var keys = optionsMonitor.CurrentValue.Data.Split(',').ToList();
                                if (keys.Contains(msg.Key))
                                {
                                    LogMonitoringLogger.LogDebug(msg.Key + ","+msg.data.ToHexString());
                                }
                            }
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
            //Task.Run(()=> {
            //    while (true)
            //    {
            //        logger.LogDebug(JsonConvert.SerializeObject(optionsMonitor.CurrentValue));
            //        Thread.Sleep(5000);
            //    }
            //});
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stop ...");
            ConsumerFactory.Unsubscribe(DispatcherConstants.DeviceMonitoringTopic);
            logger.LogInformation("Stop CompletedTask");
            return Task.CompletedTask;
        }
    }
}
