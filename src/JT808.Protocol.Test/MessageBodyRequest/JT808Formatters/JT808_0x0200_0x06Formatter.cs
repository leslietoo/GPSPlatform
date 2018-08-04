using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using JT808.Protocol.Test.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;
using JT808.Protocol.JT808Formatters;

namespace JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x06Formatter : IJT808Formatter<JT808LocationAttachImpl0x06>
    {
        public JT808LocationAttachImpl0x06 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x06 jT808LocationAttachImpl0x06 = new JT808LocationAttachImpl0x06() { };
            jT808LocationAttachImpl0x06.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x06.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x06.Age= JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            jT808LocationAttachImpl0x06.Gender = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x06.UserName = JT808BinaryExtensions.ReadStringLittle(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x06;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x06 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.Age);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.Gender);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.UserName);
            return offset;
        }
    }
}
