using GPS.GrpcServiceBase.Internal;
using GPS.IdentityServer4GrpcServer.Configs;
using GPS.IdentityServer4GrpcServer.Providers;
using GPS.IdentityServer4GrpcServiceBase;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPS.IdentityServer4GrpcServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //dotnet GPS.IdentityServer4GrpcServer.dll ASPNETCORE_ENVIRONMENT=Development
            //Environment.SetEnvironmentVariable()
            var builder = new Microsoft.Extensions.Hosting.HostBuilder()
                .UseEnvironment(args[0].Split('=')[1])
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{ hostingContext.HostingEnvironment}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                 .ConfigureLogging((context,logging) => 
                 {
                     logging.SetMinimumLevel(LogLevel.Debug);
                     logging.AddConsole();
                 })
                 .ConfigureServices((hostContext, services) =>
                 {
                     services.AddSingleton<ILoggerFactory, LoggerFactory>();
                     services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                     services.AddSingleton(hostContext.Configuration);
                     services.Configure<JwtOptions>(hostContext.Configuration.GetSection("JwtOptions"));
                     //services.AddDbContext<GPSIdentityServerDbContext>();
                     ServiceProvider build = services.BuildServiceProvider();
                     services.AddSingleton<IList<Grpc.Core.Server>>(
                         new List<Grpc.Core.Server>()
                         {
                             new Grpc.Core.Server
                             {
                                 Services = {
                                     IdentityServer4ServiceGrpc.BindService(new IdentityServer4ServiceImpl(build)),
                                 },
                                 Ports = { new ServerPort("0.0.0.0", 15500, ServerCredentials.Insecure)
                              }
                         }
                     });
                     services.AddSingleton<IHostedService, GrpcBackgroundService>();
                 });
            await builder.RunConsoleAsync();
        }
    }
}
