using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8300Formatter : IJT808Formatter<JT808_0x8300>
    {
        public JT808_0x8300 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x8300 jT808_0X8300 = new JT808_0x8300();
            jT808_0X8300.TextFlag= JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X8300.TextInfo = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X8300;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8300 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.TextFlag);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.TextInfo);
            return offset;
        }
    }
}
