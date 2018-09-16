using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using JT808.Protocol;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GPS.JT808NettyServer.Handlers
{
    /// <summary>
    /// JT808解码
    /// </summary>
    public class JT808DecodeHandler : ByteToMessageDecoder
    {
        private readonly ILogger<JT808DecodeHandler> logger;

        public JT808DecodeHandler(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<JT808DecodeHandler>();
        }

        private static long MsgSuccessCount = 0;

        private static long MsgFailCount = 0;

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            string msg = string.Empty;
            byte[] buffer = null;
            try
            {
                buffer = new byte[input.Capacity + 2];
                input.ReadBytes(buffer,1, input.Capacity);
                buffer[0] = JT808.Protocol.JT808Package.BeginFlag;
                buffer[input.Capacity + 1] = JT808.Protocol.JT808Package.EndFlag;
                output.Add(new JT808RequestInfo(buffer));
                Interlocked.Increment(ref MsgSuccessCount);
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("accept package success count<<<" + MsgSuccessCount.ToString());
                }
            }
            catch (Exception ex)
            {
                Interlocked.Increment(ref MsgFailCount);
                logger.LogError("accept package fail count<<<" + MsgFailCount.ToString());
                logger.LogError(ex, "accept msg<<<" + msg);
                return;
            }
        }
    }
}
