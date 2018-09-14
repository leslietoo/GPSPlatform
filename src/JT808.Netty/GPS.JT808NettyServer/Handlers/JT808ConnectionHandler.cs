using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Groups;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS.JT808NettyServer.Handlers
{
    public class JT808ConnectionHandler : ChannelHandlerAdapter
    {
        private readonly ILogger<JT808ConnectionHandler> logger;

        private readonly SessionManager sessionManager;

        public JT808ConnectionHandler(
            SessionManager sessionManager,
            ILoggerFactory loggerFactory)
        {
            this.sessionManager = sessionManager;
            logger = loggerFactory.CreateLogger<JT808ConnectionHandler>();
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            logger.LogDebug("<<<客户端连接服务器成功");
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            logger.LogDebug("<<<客户端与服务器断开连接");
            sessionManager.RemoveSessionByID(context.Channel.Id.AsShortText());
            base.ChannelInactive(context);
        }

        public override Task CloseAsync(IChannelHandlerContext context)
        {
            logger.LogDebug(">>>服务器断开与客户端的连接");
            return base.CloseAsync(context);
        }
    }
}
