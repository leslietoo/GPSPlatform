using Confluent.Kafka;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.MsgIdExtensions
{
    public abstract class JT808MsgIdProducerBase<TKey, TValue>: JT808MsgIdBase<TKey, TValue>
    {
        public abstract Producer<TKey, TValue> MsgIdProducer { get;}
    }
}
