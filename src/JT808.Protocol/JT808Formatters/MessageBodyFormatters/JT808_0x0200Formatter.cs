using JT808.Protocol.MessageBodyRequest;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200Formatter : IMessagePackFormatter<JT808_0x0200>
    {
        public JT808_0x0200 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            throw new NotImplementedException();
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0200 value, IFormatterResolver formatterResolver)
        {
            //offset += MessagePackBinaryExtensions.WriteUInt16(ref bytes, offset, value.AlarmFlag)
            //buffer1.Span.WriteLittle(AlarmFlag, 0, 4);
            //buffer1.Span.WriteLittle(StatusFlag, 4, 4);
            //buffer1.Span.WriteLatLng(Lat, 8);
            //buffer1.Span.WriteLatLng(Lng, 12);
            //buffer1.Span.WriteLittle(Altitude, 16, 2);
            //buffer1.Span.WriteLittle((int)(Speed * 10.0), 18, 2);
            //buffer1.Span.WriteLittle(Direction, 20, 2);
            //buffer1.Span.WriteLittle(GPSTime, 22);
            return 1;
        }
    }
}
