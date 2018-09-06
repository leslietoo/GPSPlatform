using GPS.JT808SampleDeviceMonitoring.Configs;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPS.JT808SampleDeviceMonitoring
{
    class Program
    {
        static IConfiguration configuration;

        static async Task Main(string[] args)
        {
            var serverHostBuilder = new HostBuilder()
                    .ConfigureHostConfiguration((config) =>
                    {
                        config.AddEnvironmentVariables();
                    })
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{ hostingContext.HostingEnvironment}.json", optional: true, reloadOnChange: true)
                              .AddJsonFile($"LogMonitioringOptions.json", optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                        configuration = config.Build();
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                        NLog.LogManager.LoadConfiguration("Configs/nlog.config");
                        logging.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton(configuration);
                        services.Configure<LogMonitioringOptions>(configuration.GetSection("LogMonitioringOptions"));
                        services.AddSingleton<ILoggerFactory, LoggerFactory>();
                        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                        services.AddSingleton(typeof(IServiceProvider), services.BuildServiceProvider());
                        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
                        var host = hostContext.Configuration.GetSection("KafkaOptions").GetValue<string>("bootstrap.servers");
                        services.AddSingleton(typeof(IConsumerFactory),
                            new ConsumerFactory(
                                new JT808PubSubToKafka.JT808_DeviceMonitoringDispatcher_Consumer(
                                    new Dictionary<string, object>
                                    {
                                        { "group.id", "JT808_Log_Monitoring" },
                                        { "enable.auto.commit", true },
                                        { "bootstrap.servers", host }
                                    }, loggerFactory)));
                        //services.AddSingleton<IHostedService, JT808LogMonitoringService>();
                        services.AddHostedService<JT808LogMonitoringService>();
                    });

            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
