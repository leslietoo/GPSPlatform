using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using JT808.Protocol;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

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

        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            //解决TCP粘包、拆包
            if (input.ReadableBytes <= 3)
            {
                input.ResetReaderIndex();
                return;
            }
            string msg = string.Empty;
            try
            {
                input.MarkReaderIndex();
                int length = 0;
                if (input.ReadByte() != JT808Package.BeginFlag || (length = input.BytesBefore(JT808Package.EndFlag)) == -1)
                {
                    input.ResetReaderIndex();
                    return;
                }
                input.ResetReaderIndex();
                msg = ByteBufferUtil.HexDump(input);
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    logger.LogDebug("accept msg<<<" + msg);
                }
                // 解析消息
                IByteBuffer message = Unpooled.Buffer(length + 2);
                var data = new byte[message.Array.Length];
                input.ReadBytes(data);
                // throw new Exception("test");
                output.Add(new JT808RequestInfo(data));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "accept msg<<<" + msg);
                input.ResetReaderIndex();
                return;
            }
        }
    }
}
