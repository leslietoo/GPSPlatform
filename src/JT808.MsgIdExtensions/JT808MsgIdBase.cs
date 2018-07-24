using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.MsgIdExtensions
{
    public abstract class JT808MsgIdBase<TKey, TValue>
    {
        protected JT808MsgIdBase(Dictionary<string, object> config)
        {
            Config = config;
        }

        protected JT808MsgIdBase()
        {
        }

        public abstract JT808MsgId JT808MsgId { get; }

        public virtual Dictionary<string, object> Config { get; protected set; } = new Dictionary<string, object>
        {
            {"bootstrap.servers", "172.16.19.120:9092" }
            //{"bootstrap.servers", "127.0.0.1:9092" }
        };
    }
}
