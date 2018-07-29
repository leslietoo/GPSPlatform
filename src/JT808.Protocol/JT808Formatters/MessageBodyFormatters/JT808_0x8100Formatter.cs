using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyReply;
using MessagePack;
using MessagePack.Formatters;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x8100Formatter : IMessagePackFormatter<JT808_0x8100>
    {
        public JT808_0x8100 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x8100 jT808_0X8100 = new JT808_0x8100();
            jT808_0X8100.MsgNum= JT808BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            jT808_0X8100.JT808TerminalRegisterResult =(JT808TerminalRegisterResult) JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            // 只有在成功后才有该字段
            if (jT808_0X8100.JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                jT808_0X8100.Code = JT808BinaryExtensions.ReadStringLittle(bytes, offset);
                offset = offset+ jT808_0X8100.Code.Length;
            }
            readSize = offset;
            return jT808_0X8100;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8100 value, IFormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.MsgNum);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.JT808TerminalRegisterResult);
            // 只有在成功后才有该字段
            if (value.JT808TerminalRegisterResult == JT808TerminalRegisterResult.成功)
            {
                offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.Code);
            }
            return offset;
        }
    }
}
