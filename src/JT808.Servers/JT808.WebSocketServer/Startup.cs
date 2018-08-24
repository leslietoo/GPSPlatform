using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPS.JT808PubSubToKafka;
using GPS.PubSub.Abstractions;
using JT808.MsgId0x0200Services;
using JT808.MsgId0x0200Services.Hubs;
using JT808.WebSocketServer.Hubs;
using JT808.WebSocketServer.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JT808.WebSocketServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddSignalR();
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                  builder =>
                  {
                      builder.AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowAnyOrigin()
                             //.WithOrigins("http://localhost:55830")
                             .AllowCredentials();
            }));
            var host = Configuration.GetSection("KafkaOptions").GetValue<string>("bootstrap.servers");
            var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
            services.AddSingleton(typeof(IConsumerFactory),
                new ConsumerFactory(
                    new GPS.JT808PubSubToKafka.JT808_0x0200_Consumer(
                        new Dictionary<string, object>
                        {
                            { "group.id", "JT808_0x0200_WebSocket_Alarm" },
                            { "enable.auto.commit", true },
                            { "bootstrap.servers", host }
                        }, loggerFactory)));
            services.AddSingleton<IHostedService, ToWebSocketService>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseJT808JwtVerify();
            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
                routes.MapHub<AlarmHub>("/alarmHub");
            });
        }
    }
}
