using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using JT808.Protocol.Test.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x06Formatter : IMessagePackFormatter<JT808LocationAttachImpl0x06>
    {
        public JT808LocationAttachImpl0x06 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x06 jT808LocationAttachImpl0x06 = new JT808LocationAttachImpl0x06() { };
            jT808LocationAttachImpl0x06.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x06.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x06.Age= JT808BinaryExtensions.ReadInt32Little(bytes, offset);
            offset = offset + 4;
            jT808LocationAttachImpl0x06.Gender = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x06.UserName = JT808BinaryExtensions.ReadStringLittle(bytes, offset);
            offset = offset + jT808LocationAttachImpl0x06.UserName.Length;
            readSize = offset;
            return jT808LocationAttachImpl0x06;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x06 value, IFormatterResolver formatterResolver)
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
