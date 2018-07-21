using System;
using System.IO;

namespace JT808.Protocol
{
    public abstract class JT808BufferedEntityBase
    {
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
