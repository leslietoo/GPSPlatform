using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8805Formatter : IJT808Formatter<JT808_0x8805>
    {
        public JT808_0x8805 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x8805 jT808_0X8805 = new JT808_0x8805();
            jT808_0X8805.MultimediaId = JT808BinaryExtensions.ReadInt32Little(bytes, ref offset);
            jT808_0X8805.Deleted=(JT808MultimediaDeleted)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X8805;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8805 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.MultimediaId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.Deleted);
            return offset;
        }
    }
}
