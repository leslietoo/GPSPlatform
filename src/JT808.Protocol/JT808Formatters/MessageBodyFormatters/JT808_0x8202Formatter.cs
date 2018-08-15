using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8202Formatter : IJT808Formatter<JT808_0x8202>
    {
        public JT808_0x8202 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x8202 jT808_0X8202 = new JT808_0x8202();
            jT808_0X8202.Interval = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X8202.LocationTrackingValidity = JT808BinaryExtensions.ReadInt32Little(bytes, ref offset);
            readSize = offset;
            return jT808_0X8202;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8202 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(ref bytes, offset, value.Interval);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.LocationTrackingValidity);
            return offset;
        }
    }
}
