using System;
using System.Collections.Generic;
using System.Text;
namespace JT808.Protocol
{
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
