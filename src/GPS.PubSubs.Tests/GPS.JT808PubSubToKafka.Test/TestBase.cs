using Confluent.Kafka;
using GPS.PubSub.Abstractions;
using JT808.DotNetty.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using System.Threading;

namespace GPS.JT808PubSubToKafka.Test
{
    public class TestBase
    {
        public IServiceProvider ServiceProvider { get; }

        public TestBase()
        {
            var serverHostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ILoggerFactory, LoggerFactory>();
                    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                    services.Configure<ProducerConfig>(hostContext.Configuration.GetSection("KafkaProducerConfig"));
                    services.Configure<ConsumerConfig>(hostContext.Configuration.GetSection("KafkaConsumerConfig"));
                    services.AddSingleton<JT808_MsgId_Producer>();
                    services.AddSingleton<JT808_MsgId_Consumer>();
                    services.AddSingleton<JT808_SessionPublishing_Producer>();
                    services.AddSingleton<JT808_SessionPublishing_Consumer>();
                    services.AddSingleton<JT808_UnificationPushToWebSocket_Producer>();
                    services.AddSingleton<JT808_UnificationPushToWebSocket_Consumer>();

                    //ref:http://www.cnblogs.com/catcher1994/p/handle-multi-implementations-with-same-interface-in-dotnet-core.html
                    services.AddSingleton(factory =>
                    {
                        Func<string, IJT808Producer> accesor = key =>
                        {
                            switch (key)
                            {
                                case JT808PubSubConstants.JT808TopicName:
                                    return factory.GetRequiredService<JT808_MsgId_Producer>();
                                case JT808PubSubConstants.UnificationPushToWebSocket:
                                    return factory.GetRequiredService<JT808_UnificationPushToWebSocket_Producer>();
                                default:
                                    throw new ArgumentException($"Not Support key : {key}");
                            }
                        };
                        return accesor;
                    });
                    services.AddSingleton(factory =>
                    {
                        Func<string, IJT808Consumer> accesor = key =>
                        {
                            switch (key)
                            {
                                case JT808PubSubConstants.JT808TopicName:
                                    return factory.GetRequiredService<JT808_MsgId_Consumer>();
                                case JT808PubSubConstants.UnificationPushToWebSocket:
                                    return factory.GetRequiredService<JT808_UnificationPushToWebSocket_Consumer>();
                                default:
                                    throw new ArgumentException($"Not Support key : {key}");
                            }
                        };
                        return accesor;
                    });

                    //var serviceProvider = services.BuildServiceProvider();

                    //var jT808_MsgId_Producer = serviceProvider.GetRequiredService<JT808_MsgId_Producer>();
                    //var jT808_MsgId_Consumer = serviceProvider.GetRequiredService<JT808_MsgId_Consumer>();

                    //var jT808_SessionPublishing_Producer = serviceProvider.GetRequiredService<JT808_SessionPublishing_Producer>();
                    //var jT808_SessionPublishing_Consumer = serviceProvider.GetRequiredService<JT808_SessionPublishing_Consumer>();


                    //var jT808_UnificationPushToWebSocket_Producer = serviceProvider.GetRequiredService<JT808_UnificationPushToWebSocket_Producer>();
                    //var jT808_UnificationPushToWebSocket_Consumer = serviceProvider.GetRequiredService<JT808_UnificationPushToWebSocket_Consumer>();

                    //jT808_MsgId_Consumer.Subscribe();

                    //jT808_MsgId_Consumer.OnMessage(JT808MsgId.位置信息汇报.ToValueString(), (msg) =>
                    //{

                    //});

                    //jT808_MsgId_Consumer.OnMessage(JT808MsgId.终端注册应答.ToValueString(), (msg) =>
                    //{

                    //});

                    //jT808_SessionPublishing_Consumer.OnMessage(JT808Constants.SessionOffline, (msg) =>
                    //{

                    //});

                    //jT808_SessionPublishing_Consumer.Subscribe();

                    //jT808_UnificationPushToWebSocket_Consumer.OnMessage(JT808PubSubConstants.UnificationPushToWebSocket, (msg) =>
                    //{

                    //});
                    //jT808_UnificationPushToWebSocket_Consumer.Subscribe();

                    //Thread.Sleep(3000);
                    //var data = new byte[] { 1, 2, 3, 4, 5 };
                    //var data1 = new byte[] { 1, 2, 3, 4, 5, 6 };

                    //jT808_MsgId_Producer.ProduceAsync(JT808MsgId.位置信息汇报.ToValueString(), data);
                    //jT808_MsgId_Producer.ProduceAsync(JT808MsgId.终端注册应答.ToValueString(), data1);
                    //jT808_SessionPublishing_Producer.PublishAsync(JT808Constants.SessionOffline, "13812345678,13812345679");
                    //jT808_UnificationPushToWebSocket_Producer.ProduceAsync(JT808PubSubConstants.UnificationPushToWebSocket, data);
                });
            var build=serverHostBuilder.Build();
            ServiceProvider = build.Services;
        }
    }
}
