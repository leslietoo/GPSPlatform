using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyReply;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0001Formatter : IMessagePackFormatter<JT808_0x0001>
    {
        public JT808_0x0001 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            JT808_0x0001 jT808_0X0001 = new JT808_0x0001();
            jT808_0X0001.MsgNum = BinaryExtensions.ReadUInt16Little(bytes, offset);
            jT808_0X0001.MsgId =(JT808MsgId) BinaryExtensions.ReadUInt16Little(bytes, offset);
            jT808_0X0001.JT808TerminalResult = (JT808TerminalResult)BinaryExtensions.ReadByteLittle(bytes, offset);
            readSize = offset;
            return jT808_0X0001;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0001 value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.MsgNum);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset,(ushort) value.MsgId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.JT808TerminalResult);
            return offset;
        }
    }
}
