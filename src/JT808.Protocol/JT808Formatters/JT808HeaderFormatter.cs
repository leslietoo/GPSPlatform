using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters
{
    /// <summary>
    /// JT808头部序列化器
    /// </summary>
    public class JT808HeaderFormatter : IMessagePackFormatter<JT808Header>
    {
        public JT808Header Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            throw new NotImplementedException();
        }

        public int Serialize(ref byte[] bytes, int offset, JT808Header value, IFormatterResolver formatterResolver)
        {
            offset += MessagePackBinaryExtensions.WriteUInt16(ref bytes, offset,(ushort)value.MsgId);
            offset += formatterResolver.GetFormatter<JT808MessageBodyProperty>().Serialize(ref bytes, offset, value.MessageBodyProperty, formatterResolver);
            bytes.WriteBCDLittle(value.TerminalPhoneNo, offset, 6);
            offset += 6;
            offset += MessagePackBinaryExtensions.WriteUInt16(ref bytes, offset, value.MsgNum);
            if (value.MessageBodyProperty.IsPackge)
            {
                //消息总包数2位+包序号2位=4位
                offset += 4;
            }
            return offset;
        }
    }
}
