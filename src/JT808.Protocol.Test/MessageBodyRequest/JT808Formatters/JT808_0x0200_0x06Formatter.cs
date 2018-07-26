using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;

namespace JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x06Formatter : IMessagePackFormatter<JT808LocationAttachImpl0x06>
    {
        public JT808LocationAttachImpl0x06 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x06 jT808LocationAttachImpl0x06 = new JT808LocationAttachImpl0x06() { };
            jT808LocationAttachImpl0X01.AttachInfoId = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0X01.AttachInfoLength = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0X01.Mileage= BinaryExtensions.ReadInt32Little(bytes, offset);
            offset = offset + 4;
            readSize = offset;
            return jT808LocationAttachImpl0X01;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x06 value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Mileage);
            return offset;
        }
    }
}
