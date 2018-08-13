using GPS.PubSub.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808PubSubToRabbitMQ
{
    public abstract class JT808MsgIdBase
    {
        protected JT808MsgIdBase(string conn)
        {
            ConnStr = conn;
        }

        protected JT808MsgIdBase()
        {
            ConnStr = "";
        }

        public abstract ushort CategoryId { get;}

        public string JT808MsgIdTopic => CategoryId.ToString();

        public string ConnStr { get;}
    }
}
