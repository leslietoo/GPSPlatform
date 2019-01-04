using DotNetty.Buffers;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using GPS.Dispatcher.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Handlers.Logging;
using Polly;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using GPS.JT808SourcePackageDispatcher.Configs;
using System.Linq;
using GPS.JT808SourcePackageDispatcher.Handlers;

namespace GPS.JT808SourcePackageDispatcher
{
    public class JT808SourcePackageDispatcherImpl : ISourcePackageDispatcher
    {
        private readonly ILogger<JT808SourcePackageDispatcherImpl> logger;
        private readonly ILoggerFactory loggerFactory;
        private IOptionsMonitor<RemoteServerOptions> optionsMonitor;
        public Dictionary<string, IChannel> channeldic = new Dictionary<string, IChannel>();

        public JT808SourcePackageDispatcherImpl(ILoggerFactory loggerFactory, 
                                                                            IOptionsMonitor<RemoteServerOptions> optionsMonitor)
        {
            this.loggerFactory = loggerFactory;
            logger = loggerFactory.CreateLogger<JT808SourcePackageDispatcherImpl>();
            this.optionsMonitor = optionsMonitor;
            InitialDispatcherClient();
        }
        public async Task SendAsync(byte[] data)
        {
            foreach (var item in channeldic)
            {
                try
                {
                    if (item.Value.Open)
                    {
                        await item.Value.WriteAndFlushAsync(Unpooled.WrappedBuffer(data));
                    }
                    else {
                        logger.LogError($"{item}链接已关闭");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"{item}发送数据出现异常：{ex}");
                }
            }
            await Task.CompletedTask;
        }

        public void InitialDispatcherClient()
        {
            Task.Run(async () =>
            {
                var group = new MultithreadEventLoopGroup();
                var bootstrap = new Bootstrap();
                bootstrap.Group(group)
                 .Channel<TcpSocketChannel>()
                 .Option(ChannelOption.TcpNodelay, true)
                 .Handler(new ActionChannelInitializer<ISocketChannel>(channel =>
                 {
                     IChannelPipeline pipeline = channel.Pipeline;
                     pipeline.AddLast(new ClientConnectionHandler(bootstrap, channeldic, loggerFactory));
                 }));
                optionsMonitor.OnChange(options =>
                {
                    List<string> lastRemoteServers = new List<string>();
                    if (options.RemoteServers != null)
                    {
                        lastRemoteServers = options.RemoteServers;
                    }
                    DelRemoteServsers(lastRemoteServers);
                    AddRemoteServsers(bootstrap, lastRemoteServers);
                });
                await InitRemoteServsers(bootstrap);             
            });
        }
        /// <summary>
        /// 初始化远程服务器
        /// </summary>
        /// <param name="bootstrap"></param>
        /// <param name="remoteServers"></param>
        /// <returns></returns>
        private async Task InitRemoteServsers(Bootstrap bootstrap) {
            List<string> remoteServers = new List<string>();
            if (optionsMonitor.CurrentValue.RemoteServers != null)
            {
                remoteServers = optionsMonitor.CurrentValue.RemoteServers;
            }
            foreach (var item in remoteServers)
            {
                try
                {
                    IChannel clientChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(item.Split(':')[0]), int.Parse(item.Split(':')[1])));
                    channeldic.Add(item, clientChannel);
                }
                catch (Exception ex)
                {
                    logger.LogError($"初始化配置链接远程服务端{item},链接异常：{ex}");
                }
            }
            await Task.CompletedTask;
        }
        /// <summary>
        /// 动态删除远程服务器
        /// </summary>
        /// <param name="lastRemoteServers"></param>
        private void DelRemoteServsers(List<string> lastRemoteServers) {
            var delChannels = channeldic.Keys.Except(lastRemoteServers).ToList();
            foreach (var item in delChannels)
            {
                channeldic[item].CloseAsync();
                channeldic.Remove(item);
            }
        }
        /// <summary>
        /// 动态添加远程服务器
        /// </summary>
        /// <param name="bootstrap"></param>
        /// <param name="lastRemoteServers"></param>
        private void AddRemoteServsers(Bootstrap bootstrap ,List<string> lastRemoteServers) {
            var addChannels = lastRemoteServers.Except(channeldic.Keys).ToList();
            foreach (var item in addChannels)
            {
                try
                {
                    IChannel clientChannel = bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(item.Split(':')[0]), int.Parse(item.Split(':')[1]))).Result;
                    channeldic.Add(item, clientChannel);
                }
                catch (Exception ex)
                {
                    logger.LogError($"变更配置后链接远程服务端{item},重连异常：{ex}");
                }
            }
        }
    }
}
