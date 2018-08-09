using GPS.PubSub.Abstractions;
using System;
using System.Collections.Generic;

namespace GPS.JT808PubSubToKafka
{
    public abstract class JT808MsgIdConsumerBase: JT808MsgIdBase, IConsumer
    {
        public JT808MsgIdConsumerBase()
        {
        }

        public JT808MsgIdConsumerBase(Dictionary<string, object> config) : base(config)
        {
        }

        public abstract event EventHandler<object> OnMessage;
        public abstract event EventHandler<object> OnError;
        public abstract event EventHandler<object> OnConsumeError;

        public abstract void Dispose();
        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}
