using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808PubSubToRabbitMQ.JT808RabbitMQMessage
{
    public class JT808_UnificationSend_Message
    {
        public string Key { get; set; }

        public byte[] Data { get; set; }
    }
}
