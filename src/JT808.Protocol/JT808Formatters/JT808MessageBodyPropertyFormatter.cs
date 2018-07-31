using JT808.Protocol.Enums;
using MessagePack;
using MessagePack.Formatters;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters
{
    /// <summary>
    /// 消息体属性的格式化器
    /// </summary>
    public class JT808MessageBodyPropertyFormatter : IMessagePackFormatter<JT808HeaderMessageBodyProperty>
    {
        public JT808HeaderMessageBodyProperty Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            JT808HeaderMessageBodyProperty messageBodyProperty = new JT808HeaderMessageBodyProperty();
            ReadOnlySpan<char> msgMethod = Convert.ToString(JT808BinaryExtensions.ReadUInt16Little(bytes, offset), 2).PadLeft(16, '0').AsSpan();
            messageBodyProperty.DataLength = Convert.ToInt32(msgMethod.Slice(6, 10).ToString(), 2);
            //  2.2. 数据加密方式
            switch (msgMethod.Slice(3, 3).ToString())
            {
                case "000":
                    messageBodyProperty.Encrypt = JT808EncryptMethod.None;
                    break;
                case "001":
                    messageBodyProperty.Encrypt = JT808EncryptMethod.RSA;
                    break;
                default:
                    messageBodyProperty.Encrypt = JT808EncryptMethod.None;
                    break;
            }
            messageBodyProperty.IsPackge = msgMethod[2] != '0';
            messageBodyProperty.PackgeCount = 0;
            messageBodyProperty.PackageIndex = 0;
            if (messageBodyProperty.IsPackge)
            {
                int pageCount, pageIndex;
                messageBodyProperty.PackgeCount = MessagePackBinary.ReadUInt16(bytes, offset + 8, out pageCount);
                messageBodyProperty.PackageIndex = MessagePackBinary.ReadUInt16(bytes, offset + 10, out pageIndex);
            }
            readSize = 2;
            return messageBodyProperty;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808HeaderMessageBodyProperty value, IFormatterResolver formatterResolver)
        {
            // 2.消息体属性
            Span<char> msgMethod = new char[16];
            //  2.1.保留
            msgMethod[0] = '0';
            msgMethod[1] = '0';
            //  2.2.是否分包
            msgMethod[2] = value.IsPackge ? '1' : '0';
            //  2.3.数据加密方式
            switch (value.Encrypt)
            {
                case JT808EncryptMethod.None:
                    msgMethod[3] = '0';
                    msgMethod[4] = '0';
                    msgMethod[5] = '0';
                    break;
                case JT808EncryptMethod.RSA:
                    msgMethod[3] = '0';
                    msgMethod[4] = '0';
                    msgMethod[5] = '1';
                    break;
                default:
                    msgMethod[3] = '0';
                    msgMethod[4] = '0';
                    msgMethod[5] = '0';
                    break;
            }
            //  2.4.数据长度
            ReadOnlySpan<char> dataLen = Convert.ToString(value.DataLength, 2).PadLeft(10, '0').AsSpan();
            for (int i = 1; i <= 10; i++)
            {
                msgMethod[5 + i] = dataLen[i - 1];
            }
            offset+= JT808BinaryExtensions.WriteLittle(ref bytes, offset, Convert.ToUInt16(msgMethod.ToString(), 2));
            return offset;
        }
    }
}
