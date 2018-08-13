using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToRabbitMQ
{
    public abstract class JT808MsgIdConsumerBase: JT808MsgIdBase, IConsumer
    {
        public JT808MsgIdConsumerBase(ILoggerFactory loggerFactory)
        {
        }

        public JT808MsgIdConsumerBase(string connStr, ILoggerFactory loggerFactory) : base(connStr)
        {
        }

        public CancellationTokenSource Cts { get; }
        protected abstract ILogger Logger { get; }
        public abstract void OnMessage(Action<(string Key, byte[] data)> callback);
        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}
