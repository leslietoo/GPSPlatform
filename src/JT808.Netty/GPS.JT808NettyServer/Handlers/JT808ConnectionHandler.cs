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
            logger.LogDebug("<<<Successful client connection to server");
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            logger.LogDebug("<<<The client disconnects from the server");
            sessionManager.RemoveSessionByID(context.Channel.Id.AsShortText());
            base.ChannelInactive(context);
        }

        public override Task CloseAsync(IChannelHandlerContext context)
        {
            logger.LogDebug(">>>The server disconnects from the client");
            return base.CloseAsync(context);
        }
    }
}
