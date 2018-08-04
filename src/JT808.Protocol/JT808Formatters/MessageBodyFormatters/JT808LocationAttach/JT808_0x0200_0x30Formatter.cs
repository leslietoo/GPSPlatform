using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x30Formatter : IJT808Formatter<JT808LocationAttachImpl0x30>
    {
        public JT808LocationAttachImpl0x30 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x30 jT808LocationAttachImpl0x30 = new JT808LocationAttachImpl0x30();
            jT808LocationAttachImpl0x30.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x30.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x30.WiFiSignalStrength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x30;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x30 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.WiFiSignalStrength);
            return offset;
        }
    }
}
