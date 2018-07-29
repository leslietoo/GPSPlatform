using MessagePack;
using System;
using System.Runtime.Serialization;

namespace JT808.Protocol
{
    [MessagePackObject]
    public abstract class JT808BufferedEntityBase
    {
        [IgnoreDataMember]
        public Memory<byte> Buffer { get; protected set; }

        protected JT808BufferedEntityBase(Memory<byte> buffer)
        {
            Buffer = buffer;
        }

        protected JT808BufferedEntityBase()
        {

        }

        public abstract void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs);

        public abstract void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs);
    }
}
