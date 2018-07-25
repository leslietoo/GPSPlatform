using JT808.Protocol.JT808Formatters;
using MessagePack;
using System;

namespace JT808.Protocol
{
    [MessagePackObject]
    public abstract class JT808Bodies : JT808BufferedEntityBase
    {
        public JT808Bodies(Memory<byte> buffer) : base(buffer)
        {

        }

        public JT808Bodies()
        {

        }
    }
}
