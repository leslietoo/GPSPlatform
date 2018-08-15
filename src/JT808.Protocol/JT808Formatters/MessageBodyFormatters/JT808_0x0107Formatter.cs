using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0107Formatter : IJT808Formatter<JT808_0x0107>
    {
        public JT808_0x0107 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0107 jT808_0X0107 = new JT808_0x0107();
            jT808_0X0107.TerminalType = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X0107.MakerId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 5);
            jT808_0X0107.TerminalModel = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 20);
            jT808_0X0107.TerminalId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 7);
            jT808_0X0107.Terminal_SIM_ICCID = JT808BinaryExtensions.ReadBCD(bytes, ref offset, 5).ToString();
            jT808_0X0107.Terminal_Hardware_Version_Length= JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0107.Terminal_Hardware_Version_Num = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X0107.Terminal_Hardware_Version_Length);
            jT808_0X0107.Terminal_Firmware_Version_Length = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0107.Terminal_Firmware_Version_Num = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, jT808_0X0107.Terminal_Firmware_Version_Length);
            jT808_0X0107.GNSSModule = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0107.CommunicationModule = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0107;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0107 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(ref bytes, offset, value.TerminalType);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.MakerId.PadRight(5, '0'));
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.TerminalModel.PadRight(20, '0'));
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.TerminalId.PadRight(7, '0'));
            offset += JT808BinaryExtensions.WriteBCDLittle(ref bytes, offset, value.Terminal_SIM_ICCID,5,10);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset,(byte)value.Terminal_Hardware_Version_Num.Length);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.Terminal_Hardware_Version_Num);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.Terminal_Firmware_Version_Num.Length);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.Terminal_Firmware_Version_Num);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.GNSSModule);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.CommunicationModule);
            return offset;
        }
    }
}
