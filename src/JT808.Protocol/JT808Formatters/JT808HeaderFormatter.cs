using JT808.Protocol.Enums;
using MessagePack;
using MessagePack.Formatters;
using JT808.Protocol.Extensions;
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
            JT808Header jT808Header = new JT808Header();
            // 1.消息ID
            jT808Header.MsgId =(JT808MsgId)JT808BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            // 2.消息体属性
            jT808Header.MessageBodyProperty = formatterResolver.GetFormatter<JT808MessageBodyProperty>().Deserialize(bytes, offset, formatterResolver, out readSize);
            offset = offset + 2;
            // 3.终端手机号 (写死大陆手机号码)
            jT808Header.TerminalPhoneNo = JT808BinaryExtensions.ReadBCD(bytes, offset, 6).ToString().PadLeft(12, '0');
            offset = offset + 6;
            // 4.消息流水号
            jT808Header.MsgNum = JT808BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            readSize = offset;
            return jT808Header;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808Header value, IFormatterResolver formatterResolver)
        {
            // 1.消息ID
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (ushort)value.MsgId);
            //2.消息体属性
            offset = formatterResolver.GetFormatter<JT808MessageBodyProperty>().Serialize(ref bytes, offset, value.MessageBodyProperty, formatterResolver);
            // 3.终端手机号 (写死大陆手机号码)
            offset += JT808BinaryExtensions.WriteBCDLittle(ref bytes, value.TerminalPhoneNo, offset, 6);
            //消息流水号
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.MsgNum);
            if (value.MessageBodyProperty.IsPackge)
            {
                //消息总包数2位+包序号2位=4位
                offset += 4;
            }
            return offset;
        }
    }
}
