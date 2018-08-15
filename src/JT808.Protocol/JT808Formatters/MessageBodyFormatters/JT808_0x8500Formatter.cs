using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8500Formatter : IJT808Formatter<JT808_0x8500>
    {
        public JT808_0x8500 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x8500 jT808_0X8500 = new JT808_0x8500();
            jT808_0X8500.ControlFlag= JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X8500;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8500 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.ControlFlag);
            return offset;
        }
    }
}
