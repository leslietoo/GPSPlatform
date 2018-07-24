using JT808.MsgIdExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace JT808.MsgId0x0200Service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //JT808_0x0200_Consumer jT808_0X0200_Consumer = new JT808_0x0200_Consumer(new Dictionary<string, object>
            //{
            //    { "group.id", "JT808_0x0200" },
            //    { "enable.auto.commit", true },  // this is the default
            //    { "auto.commit.interval.ms", 5000 },
            //    { "statistics.interval.ms", 60000 },
            //    { "session.timeout.ms", 6000 },
            //    { "auto.offset.reset", "smallest" },
            //    { "bootstrap.servers", "172.16.19.120:9092" }
            //});
            //jT808_0X0200_Consumer.MsgIdConsumer.OnMessage += (_, msg) =>
            //{
            //    // todo: 处理定位数据


            //   Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
            //};
            //jT808_0X0200_Consumer.MsgIdConsumer.OnError += (_, error) =>
            //{
            //    Console.WriteLine($"Error: {error}");
            //};
            //jT808_0X0200_Consumer.MsgIdConsumer.OnConsumeError += (_, msg) =>
            //{
            //    Console.WriteLine($"Error consuming from topic/partition/offset {msg.Topic}/{msg.Partition}/{msg.Offset}: {msg.Error}");
            //};
            //jT808_0X0200_Consumer.MsgIdConsumer.Subscribe("512");

            //var cancelled = false;
            //Console.CancelKeyPress += (_, e) => {
            //    e.Cancel = true;  // prevent the process from terminating.
            //    cancelled = true;
            //};
            //Console.WriteLine("Ctrl-C to exit.");
            //while (!cancelled)
            //{
            //    jT808_0X0200_Consumer.MsgIdConsumer.Poll(TimeSpan.FromMilliseconds(100));
            //    //if (jT808_0X0200_Consumer.MsgIdConsumer.Consume(out Message<Ignore, string> msg, TimeSpan.FromSeconds(1)))
            //    //{
            //    //    Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
            //    //}
            //}

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
                            { "group.id", "JT808_0x0200" },
                            { "enable.auto.commit", true },
                            { "bootstrap.servers", host }
                        }));
                        services.AddScoped<IHostedService, MsgId0x0200Service>();
                    });

            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
