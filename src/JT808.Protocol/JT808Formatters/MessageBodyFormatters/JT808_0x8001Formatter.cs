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
    public class JT808_0x8001Formatter : IMessagePackFormatter<JT808_0x8001>
    {
        public JT808_0x8001 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x8001 jT808_0X8001 = new JT808_0x8001();
            jT808_0X8001.MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            jT808_0X8001.MsgId = (JT808MsgId)JT808BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            jT808_0X8001.JT808PlatformResult = (JT808PlatformResult)JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            readSize = offset;
            return jT808_0X8001;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x8001 value, IFormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.MsgNum);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (ushort)value.MsgId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.JT808PlatformResult);
            return offset;
        }
    }
}
