using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public abstract class JT808MsgIdConsumerBase: JT808MsgIdBase, IConsumer
    {
        public JT808MsgIdConsumerBase(ILoggerFactory loggerFactory)
        {
            Cts = new CancellationTokenSource();
        }

        public JT808MsgIdConsumerBase(Dictionary<string, object> config, ILoggerFactory loggerFactory) : base(config)
        {
            Cts = new CancellationTokenSource();
        }

        public CancellationTokenSource Cts { get; }
        protected abstract ILogger Logger { get; }
        public abstract void OnMessage(Action<(string Key, byte[] data)> callback);
        protected abstract void RegisterEvent();
        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}
