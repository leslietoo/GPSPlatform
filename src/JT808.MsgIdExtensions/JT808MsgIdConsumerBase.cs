using Confluent.Kafka;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.MsgIdExtensions
{
    public abstract class JT808MsgIdConsumerBase<TKey, TValue>: JT808MsgIdBase<TKey, TValue>
    {
        protected JT808MsgIdConsumerBase()
        {
        }

        protected JT808MsgIdConsumerBase(Dictionary<string, object> config) : base(config)
        {
        }

        public abstract Consumer<TKey, TValue> MsgIdConsumer { get; set; }
    }
}
