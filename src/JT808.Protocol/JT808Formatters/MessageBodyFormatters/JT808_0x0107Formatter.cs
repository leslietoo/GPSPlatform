using JT808.Protocol.MessageBodyReply;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0107Formatter : IJT808Formatter<JT808_0x0107>
    {
        public JT808_0x0107 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            throw new NotImplementedException();
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0107 value, IJT808FormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }
    }
}
