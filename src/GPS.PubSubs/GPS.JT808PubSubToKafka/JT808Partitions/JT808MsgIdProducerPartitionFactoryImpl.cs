using Confluent.Kafka.Admin;
using GPS.PubSub.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPS.JT808PubSubToKafka.JT808Partitions
{
    /// <summary>
    /// 根据设备终端号进行分区策略
    /// </summary>
    public class JT808MsgIdProducerPartitionFactoryImpl : IJT808ProducerPartitionFactory
    {
        public int CreatePartition(string topicName, string msgId, string terminalNo)
        {
            var key1Byte1 = JT808HashAlgorithm.ComputeMd5(terminalNo);
            var p = JT808HashAlgorithm.Hash(key1Byte1, 2) % 8;
            return (int)p;
        }
    }
}
