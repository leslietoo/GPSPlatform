using GPS.PubSub.Abstractions;
using JT808.MsgId0x0200Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToRabbitMQ.Consumer
{
    class Program
    {
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
                              .AddJsonFile($"appsettings.{ hostingContext.HostingEnvironment}.json", optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                        NLog.LogManager.LoadConfiguration("Configs/nlog.config");
                        logging.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<ILoggerFactory, LoggerFactory>();
                        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
                        services.AddSingleton(typeof(IConsumerFactory),
                            new ConsumerFactory(
                                new GPS.JT808PubSubToRabbitMQ.JT808_0x0200_Consumer("host=172.16.19.120"
                                    , loggerFactory)));
                        services.AddScoped<IHostedService, ToDatabaseService>();
                    });

            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
