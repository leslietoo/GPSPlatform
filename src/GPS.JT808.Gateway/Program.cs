using Confluent.Kafka;
using GPS.JT808.Gateway.MsgIdsHandlers;
using GPS.JT808PubSubToKafka;
using GPS.JT808SourcePackageDispatcher;
using GPS.JT808SourcePackageDispatcher.Configs;
using GPS.PubSub.Abstractions;
using JT808.DotNetty.Abstractions;
using JT808.DotNetty.Core;
using JT808.DotNetty.Core.Handlers;
using JT808.DotNetty.Tcp;
using JT808.DotNetty.Udp;
using JT808.DotNetty.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GPS.JT808.Gateway
{
    class Program
    {
        static async Task Main(string[] args)
        {
               var serverHostBuilder = new HostBuilder()
               .UseEnvironment(args[0].Split('=')[1])
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{ hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging((context, logging) =>
                {
                    if (Environment.OSVersion.Platform == PlatformID.Unix)
                    {
                        NLog.LogManager.LoadConfiguration("Configs/nlog.unix.config");
                    }
                    else {
                        NLog.LogManager.LoadConfiguration("Configs/nlog.win.config");
                    }
                    logging.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ILoggerFactory, LoggerFactory>();
                    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                    services.Configure<RemoteServerOptions>(hostContext.Configuration.GetSection("RemoteServerOptions"));
                    services.Configure<ProducerConfig>(hostContext.Configuration.GetSection("KafkaProducerConfig"));
                    services.Configure<ConsumerConfig>(hostContext.Configuration.GetSection("KafkaConsumerConfig"));
                    services.AddSingleton<IJT808Producer, JT808_MsgId_Producer>();

                    // 自定义Tcp消息处理业务
                    services.Replace(new ServiceDescriptor(typeof(JT808MsgIdTcpHandlerBase), typeof(JT808MsgIdTcpCustomHandler), ServiceLifetime.Singleton));
                    // 自定义Udp消息处理业务
                    services.Replace(new ServiceDescriptor(typeof(JT808MsgIdUdpHandlerBase), typeof(JT808MsgIdUdpCustomHandler), ServiceLifetime.Singleton));
                    
                    services.Replace(new ServiceDescriptor(typeof(IJT808SessionPublishing), typeof(JT808_SessionPublishing_Producer), ServiceLifetime.Singleton));
                    //原包转发
                    services.Replace(new ServiceDescriptor(typeof(IJT808SourcePackageDispatcher), typeof(JT808SourcePackageDispatcherImpl), ServiceLifetime.Singleton));

                    services.AddJT808Core(hostContext.Configuration)
                    .AddJT808TcpHost()
                    .AddJT808UdpHost()
                    .AddJT808WebApiHost();
                });

            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
