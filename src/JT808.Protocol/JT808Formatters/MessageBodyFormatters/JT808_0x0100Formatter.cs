using JT808.Protocol.MessageBodyRequest;
using MessagePack;
using MessagePack.Formatters;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0100Formatter : IMessagePackFormatter<JT808_0x0100>
    {
        public JT808_0x0100 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0100 jT808_0X0100 = new JT808_0x0100();
            jT808_0X0100.AreaID = JT808BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            jT808_0X0100.CityOrCountyId = JT808BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            jT808_0X0100.MakerId = JT808BinaryExtensions.ReadStringLittle(bytes, offset,5);
            offset = offset + 5;
            jT808_0X0100.TerminalType = JT808BinaryExtensions.ReadStringLittle(bytes, offset, 20);
            offset = offset + 20;
            jT808_0X0100.TerminalId = JT808BinaryExtensions.ReadStringLittle(bytes, offset, 7);
            offset = offset + 7;
            jT808_0X0100.PlateColor = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808_0X0100.PlateNo = JT808BinaryExtensions.ReadStringLittle(bytes, offset);
            offset = offset + jT808_0X0100.PlateNo.Length;
            readSize = offset;
            return jT808_0X0100;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0100 value, IFormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AreaID);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.CityOrCountyId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.MakerId.PadRight(5, '0'));
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.TerminalType.PadRight(20,'0'));
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.TerminalId.PadRight(7, '0'));
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.PlateColor);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.PlateNo);
            return offset;
        }
    }
}
