using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x04Formatter : IJT808Formatter<JT808LocationAttachImpl0x04>
    {
        public JT808LocationAttachImpl0x04 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x04 jT808LocationAttachImpl0x04 = new JT808LocationAttachImpl0x04();
            jT808LocationAttachImpl0x04.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x04.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x04.EventId = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x04;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x04 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteUInt16Little(ref bytes, offset, value.EventId);
            return offset;
        }
    }
}
