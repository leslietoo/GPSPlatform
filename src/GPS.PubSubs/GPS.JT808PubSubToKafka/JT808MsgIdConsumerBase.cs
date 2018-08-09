using GPS.PubSub.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public abstract class JT808MsgIdConsumerBase: JT808MsgIdBase, IConsumer
    {
        public JT808MsgIdConsumerBase()
        {
            Cts = new CancellationTokenSource();
        }

        public JT808MsgIdConsumerBase(Dictionary<string, object> config) : base(config)
        {
            Cts = new CancellationTokenSource();
        }

        public abstract event EventHandler<object> OnMessage;
        public abstract event EventHandler<object> OnError;
        public abstract event EventHandler<object> OnConsumeError;
        public CancellationTokenSource Cts { get; }
        public abstract void Dispose();
        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}
