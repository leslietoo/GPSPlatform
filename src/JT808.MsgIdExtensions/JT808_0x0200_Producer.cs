using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.MsgIdExtensions
{
    public class JT808_0x0200_Producer : JT808MsgIdProducerBase<Null, string>
    {
        public override JT808MsgId JT808MsgId => JT808MsgId.位置信息汇报;

        public override Producer<Null, string> MsgIdProducer => new Producer<Null, string>(Config, null, new StringSerializer(Encoding.UTF8));
    }
}
