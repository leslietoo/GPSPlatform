using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x30Formatter : IMessagePackFormatter<JT808LocationAttachImpl0x30>
    {
        public JT808LocationAttachImpl0x30 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x30 jT808LocationAttachImpl0x30 = new JT808LocationAttachImpl0x30();
            jT808LocationAttachImpl0x30.AttachInfoId = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x30.AttachInfoLength = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x30.WiFiSignalStrength = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            readSize = offset;
            return jT808LocationAttachImpl0x30;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x30 value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.WiFiSignalStrength);
            return offset;
        }
    }
}
