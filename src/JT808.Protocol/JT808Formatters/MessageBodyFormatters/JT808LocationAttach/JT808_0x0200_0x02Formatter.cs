using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x02Formatter : IMessagePackFormatter<JT808LocationAttachImpl0x02>
    {
        public JT808LocationAttachImpl0x02 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x02 jT808LocationAttachImpl0X02 = new JT808LocationAttachImpl0x02();
            jT808LocationAttachImpl0X02.AttachInfoId = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0X02.AttachInfoLength = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0X02.Oil = BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            readSize = offset;
            return jT808LocationAttachImpl0X02;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x02 value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Oil);
            return offset;
        }
    }
}
