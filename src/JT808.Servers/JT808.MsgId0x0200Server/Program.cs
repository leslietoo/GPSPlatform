using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JT808.MsgIdExtensions;
using JT808.MsgId0x0200Services;
using Newtonsoft.Json;

namespace JT808.MsgId0x0200Service
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
                        var host = hostContext.Configuration.GetSection("KafkaOptions").GetValue<string>("bootstrap.servers");
                        services.AddSingleton(new JT808_0x0200_Consumer(new Dictionary<string, object>
                        {
                            { "group.id", "JT808_0x0200_ToDatabese" },
                            { "enable.auto.commit", true },
                            { "bootstrap.servers", host }
                        }));
                        services.AddScoped<IHostedService, ToDatabaseService>();
                    });

            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
