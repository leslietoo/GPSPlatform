using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x2BFormatter : IMessagePackFormatter<JT808LocationAttachImpl0x2B>
    {
        public JT808LocationAttachImpl0x2B Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x2B jT808LocationAttachImpl0x2B = new JT808LocationAttachImpl0x2B();
            jT808LocationAttachImpl0x2B.AttachInfoId = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x2B.AttachInfoLength = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x2B.Analog = BinaryExtensions.ReadInt32Little(bytes, offset);
            offset = offset + 4;
            readSize = offset;
            return jT808LocationAttachImpl0x2B;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x2B value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Analog);
            return offset;
        }
    }
}
