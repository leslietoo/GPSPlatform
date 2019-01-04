using DotNetty.Common.Internal.Logging;
using DotNetty.Transport.Channels;
using GPS.Dispatcher.Abstractions;
using GPS.JT808NettyServer.Configs;
using GPS.JT808NettyServer.Handlers;
using GPS.JT808SourcePackageDispatcher;
using GPS.JT808SourcePackageDispatcher.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NLog.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GPS.JT808NettyServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Environment.SetEnvironmentVariable("io.netty.allocator.numDirectAremas","0");
            var serverHostBuilder = new HostBuilder()
                    .UseEnvironment(args[0].Split('=')[1])
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{ hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                        //logging.AddConsole();
                        NLog.LogManager.LoadConfiguration("Configs/nlog.config");
                        logging.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<ILoggerFactory, LoggerFactory>();
                        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                        services.Configure<NettyOptions>(hostContext.Configuration.GetSection("NettyOptions"));
                        services.Configure<RemoteServerOptions>(hostContext.Configuration.GetSection("RemoteServerOptions"));
                        services.Configure<NettyIdleStateOptions>(hostContext.Configuration.GetSection("NettyIdleStateOptions"));
                        services.AddSingleton<SessionManager, SessionManager>();
                        services.AddSingleton<JT808MsgIdHandler, JT808MsgIdHandler>();
                        services.AddScoped<JT808ConnectionHandler, JT808ConnectionHandler>();
                        services.AddScoped<JT808DecodeHandler, JT808DecodeHandler>();
                        services.AddScoped<JT808ServiceHandler, JT808ServiceHandler>();
                        services.AddSingleton<ISourcePackageDispatcher, JT808SourcePackageDispatcherImpl>();
                        services.AddSingleton<IHostedService, JT808NettyService>();  
                    });

            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
