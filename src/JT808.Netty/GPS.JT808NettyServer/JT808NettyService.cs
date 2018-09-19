using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Libuv;
using GPS.JT808NettyServer.Handlers;
using GPS.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using GPS.JT808NettyServer.Configs;
using Microsoft.Extensions.Options;
using DotNetty.Handlers.Timeout;

namespace GPS.JT808NettyServer
{
    public class JT808NettyService : BackgroundService
    {
        public override string ServiceName => "JT808NettyService";

        IEventLoopGroup bossGroup;

        IEventLoopGroup workerGroup;

        IChannel boundChannel;

        readonly IServiceProvider serviceProvider;

        readonly NettyOptions nettyOptions;

        public JT808NettyService(
            IOptions<NettyOptions> nettyOptionsAccessor,
            IServiceProvider serviceProvider)
        {
            nettyOptions = nettyOptionsAccessor.Value;
            this.serviceProvider= serviceProvider;
        }

        protected override Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var dispatcher = new DispatcherEventLoopGroup();
                bossGroup = dispatcher;
                workerGroup = new WorkerEventLoopGroup(dispatcher);
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workerGroup);
                bootstrap.Channel<TcpServerChannel>();
                bootstrap
                       //.Handler(new LoggingHandler("SRV-LSTN"))
                       .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                       {
                           InitChannel(channel);
                       }))
                       .Option(ChannelOption.SoBacklog, 1048576);
                if (nettyOptions.Host == "")
                {
                    boundChannel = bootstrap.BindAsync(nettyOptions.Port).Result;
                }
                else
                {
                    boundChannel = bootstrap.BindAsync(nettyOptions.Host, nettyOptions.Port).Result;
                }
            }
            catch (Exception ex)
            {

            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// handler报错：
        /// is not a @Sharable handler, so can't be added or removed multiple times.
        /// 两种解决方法：
        /// 1.加上注解@Sharable（未实现待测试）
        /// 2.每次都new一个新的handler的实例
        /// </summary>
        /// <param name="channel"></param>
        private void InitChannel(IChannel channel)
        {
            var scope=serviceProvider.CreateScope();
            IOptionsMonitor<NettyIdleStateOptions> options = scope.ServiceProvider.GetRequiredService<IOptionsMonitor<NettyIdleStateOptions>>();
            channel.Pipeline.AddLast("systemIdleState", new IdleStateHandler(options.CurrentValue.ReaderIdleTimeSeconds, options.CurrentValue.WriterIdleTimeSeconds, options.CurrentValue.AllIdleTimeSeconds));
            channel.Pipeline.AddLast("jt808Connection", scope.ServiceProvider.GetRequiredService<JT808ConnectionHandler>());
            channel.Pipeline.AddLast("jt808Buffer",new DelimiterBasedFrameDecoder(int.MaxValue, Unpooled.CopiedBuffer(new byte[] { JT808.Protocol.JT808Package.BeginFlag }),Unpooled.CopiedBuffer(new byte[] { JT808.Protocol.JT808Package.EndFlag })));
            channel.Pipeline.AddLast("jt808Decode", scope.ServiceProvider.GetRequiredService<JT808DecodeHandler>());
            channel.Pipeline.AddLast("jt808Service", scope.ServiceProvider.GetRequiredService<JT808ServiceHandler>());
            scope.Dispose();
        }

        protected override Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                Task.WhenAll(
                  bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                  workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                  boundChannel.CloseAsync());
            }
            catch (Exception ex)
            {

            }
            return Task.CompletedTask;
        }
    }
}
