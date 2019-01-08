using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.PubSub.Abstractions
{
    /// <summary>
    /// jt808生产者分区工厂
    /// 分区策略：
    /// 1.可以根据设备终端号进行分区
    /// 2.可以根据msgId(消息Id)+设备终端号进行分区
    /// </summary>
    public interface IJT808ProducerPartitionFactory
    {
        int CreatePartition(string topicName, string msgId, string terminalNo);
    }
}
