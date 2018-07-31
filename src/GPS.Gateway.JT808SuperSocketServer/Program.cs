using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JT808.MsgIdExtensions;

namespace GPS.Gateway.JT808SuperSocketServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
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
                                services.AddSingleton<JT808MsgIdHandler>();
                                services.AddSingleton<SuperSocketNLogFactoryExtensions>();
                                var host = hostContext.Configuration.GetSection("KafkaOptions").GetValue<string>("bootstrap.servers");
                                services.Configure<SuperSocketOptions>(hostContext.Configuration.GetSection("SuperSocketOptions"));
                                services.AddSingleton(new JT808_0x0200_Producer(new Dictionary<string, object>
                                {
                                    { "bootstrap.servers", host }
                                }));
                                services.AddSingleton(new JT808_UnificationSend_Consumer(new Dictionary<string, object>
                                {
                                    { "group.id", "GatewayUnificationSend" },
                                    { "enable.auto.commit", true },
                                    { "bootstrap.servers", host }
                                }));
                                services.AddSingleton<JT808Server>();
                                services.AddScoped<IHostedService, JT808Service>();
                            });
                await serverHostBuilder.RunConsoleAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.ToString());
            }
        }
    }
}
