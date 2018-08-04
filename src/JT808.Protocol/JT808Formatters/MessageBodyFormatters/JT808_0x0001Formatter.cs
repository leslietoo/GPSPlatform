using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyReply;
using JT808.Protocol.Extensions;
using System;


namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0001Formatter :  IJT808Formatter<JT808_0x0001>
    {
        public JT808_0x0001 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0001 jT808_0X0001 = new JT808_0x0001();
            jT808_0X0001.MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808_0X0001.MsgId = (JT808MsgId)JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            jT808_0X0001.JT808TerminalResult = (JT808TerminalResult)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0001;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0001 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(ref bytes, offset, value.MsgNum);
            offset += JT808BinaryExtensions.WriteUInt16Little(ref bytes, offset, (ushort)value.MsgId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.JT808TerminalResult);
            return offset;
        }
    }
}
