using DotNetty.Transport.Channels;
using GPS.JT808NettyServer.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GPS.JT808NettyServer
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
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{ hostingContext.HostingEnvironment}.json", optional: true, reloadOnChange: true);
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                        logging.AddConsole();
                        //NLog.LogManager.LoadConfiguration("Configs/nlog.config");
                        //logging.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<ILoggerFactory, LoggerFactory>();
                        services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                        services.AddSingleton(services.BuildServiceProvider());
                        services.AddSingleton<SessionManager, SessionManager>();
                        services.AddSingleton<JT808MsgIdHandler, JT808MsgIdHandler>();
                        services.AddScoped<JT808ConnectionHandler, JT808ConnectionHandler>();
                        services.AddScoped<JT808DecodeHandler, JT808DecodeHandler>();
                        services.AddScoped<JT808ServiceHandler, JT808ServiceHandler>();
                        services.AddSingleton<IHostedService, JT808NettyService>(); 
                    });

            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
