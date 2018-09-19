using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using GPS.JT808NettyServer.Configs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace GPS.JT808NettyServer.Handlers
{
    public class JT808ConnectionHandler : ChannelHandlerAdapter
    {
        private readonly ILogger<JT808ConnectionHandler> logger;

        private readonly SessionManager sessionManager;

        private IOptionsMonitor<NettyOptions> optionsMonitor;

        public JT808ConnectionHandler(
            IOptionsMonitor<NettyOptions> optionsMonitor,
            SessionManager sessionManager,
            ILoggerFactory loggerFactory)
        {
            this.optionsMonitor = optionsMonitor;
            this.sessionManager = sessionManager;
            logger = loggerFactory.CreateLogger<JT808ConnectionHandler>();
        }

        /// <summary>
        /// 通道激活
        /// 1.判断是否启用白名单
        /// 2.白名单列表
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelActive(IChannelHandlerContext context)
        {
            if (optionsMonitor.CurrentValue.IpWhiteListDisabled)
            {
                string ip = context.Channel.RemoteAddress.ToString();
                string channelId = context.Channel.Id.AsShortText();
                if (!optionsMonitor.CurrentValue.IpWhiteList.Contains(ip))
                {
                    logger.LogInformation($"<<<Fail client connection to server. remote ip>>>{ip}");
                    CloseAsync(context);
                    return;
                }
            }
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("<<<Successful client connection to server.");
            }
            base.ChannelActive(context);
        }

        /// <summary>
        /// 设备主动断开
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug(">>>The client disconnects from the server.");
            sessionManager.RemoveSessionByID(context.Channel.Id.AsShortText());
            base.ChannelInactive(context);
        }
        /// <summary>
        /// 服务器主动断开
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task CloseAsync(IChannelHandlerContext context)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug("<<<The server disconnects from the client.");
            return base.CloseAsync(context);
        }

        /// <summary>
        /// 超时策略
        /// </summary>
        /// <param name="context"></param>
        /// <param name="evt"></param>
        public override void UserEventTriggered(IChannelHandlerContext context, object evt)
        {
            IdleStateEvent idleStateEvent = evt as IdleStateEvent;
            if (idleStateEvent != null)
            {
                string channelId = context.Channel.Id.AsShortText();
                logger.LogInformation($"{idleStateEvent.State.ToString()}>>>{channelId}");
                // 由于808是设备发心跳，如果很久没有上报数据，那么就由服务器主动关闭连接。
                context.CloseAsync();
                //switch (idleStateEvent.State)
                //{
                //    case IdleState.ReaderIdle:

                //        break;
                //    case IdleState.WriterIdle:

                //        break;
                //    case IdleState.AllIdle:

                //        break;
                //    default:

                //        break;
                //}
            }
            base.UserEventTriggered(context, evt);
        }
    }
}
