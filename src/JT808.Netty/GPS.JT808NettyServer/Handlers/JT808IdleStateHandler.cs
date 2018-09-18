using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808NettyServer.Handlers
{
    public class JT808IdleStateHandler : IdleStateHandler
    {
        private readonly ILogger<JT808IdleStateHandler> logger;

        private readonly SessionManager sessionManager;

        //public JT808IdleStateHandler(
        //    SessionManager sessionManager)
        //{
        //    this.sessionManager = sessionManager;

        //}

        public JT808IdleStateHandler(int readerIdleTimeSeconds, int writerIdleTimeSeconds,  int allIdleTimeSeconds) :
            base(readerIdleTimeSeconds, writerIdleTimeSeconds, allIdleTimeSeconds)
        {
        }

        public JT808IdleStateHandler(TimeSpan readerIdleTime, TimeSpan writerIdleTime, TimeSpan allIdleTime) :
            base(readerIdleTime, writerIdleTime, allIdleTime)
        {
        }

        public JT808IdleStateHandler(bool observeOutput, TimeSpan readerIdleTime, TimeSpan writerIdleTime, TimeSpan allIdleTime) 
            : base(observeOutput, readerIdleTime, writerIdleTime, allIdleTime)
        {
        }

        protected override void ChannelIdle(IChannelHandlerContext context, IdleStateEvent stateEvent)
        {
            if (stateEvent.State == IdleState.ReaderIdle){
                context.CloseAsync();
            }
            base.ChannelIdle(context, stateEvent);
        }
    }
}
