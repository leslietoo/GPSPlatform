using JT808.Protocol.MessageBodyRequest;
using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0102Formatter : IJT808Formatter<JT808_0x0102>
    {
        public JT808_0x0102 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0102 jT808_0X0102 = new JT808_0x0102();
            jT808_0X0102.Code = JT808BinaryExtensions.ReadStringLittle(bytes,ref offset);
            readSize = offset;
            return jT808_0X0102;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0102 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.Code);
            return offset;
        }
    }
}
