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
            Config = new Dictionary<string, object>
            {
                {"bootstrap.servers", "172.16.19.120:9092" }
                //{"bootstrap.servers", "127.0.0.1:9092" }
            };
            foreach(var item in config)
            {
                if (Config.ContainsKey(item.Key))
                {
                    Config[item.Key]= item.Value;
                }
                else
                {
                    Config.Add(item.Key, item.Value);
                }
            }
        }

        protected JT808MsgIdBase()
        {
            Config = new Dictionary<string, object>
            {
                {"bootstrap.servers", "172.16.19.120:9092" }
                //{"bootstrap.servers", "127.0.0.1:9092" }
            };
        }

        public abstract JT808MsgId JT808MsgId { get; }

        public string JT808MsgIdTopic => ((ushort)JT808MsgId).ToString();

        public Dictionary<string, object> Config { get;} 
    }
}
