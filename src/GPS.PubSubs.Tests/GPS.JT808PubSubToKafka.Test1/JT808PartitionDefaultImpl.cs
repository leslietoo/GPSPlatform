using GPS.PubSub.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808PubSubToKafka.Test
{
    public class JT808PartitionDefaultImpl : IJT808Partition
    {
        public JT808PartitionDefaultImpl(string terminalNo)
        {
            TerminalNo = terminalNo;
        }

        public string TerminalNo { get; }
    }
}
