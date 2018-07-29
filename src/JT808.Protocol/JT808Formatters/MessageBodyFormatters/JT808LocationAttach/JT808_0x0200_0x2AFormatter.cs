using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x2AFormatter : IMessagePackFormatter<JT808LocationAttachImpl0x2A>
    {
        public JT808LocationAttachImpl0x2A Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x2A jT808LocationAttachImpl0X2A = new JT808LocationAttachImpl0x2A();
            jT808LocationAttachImpl0X2A.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0X2A.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0X2A.IOStatus = JT808BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            readSize = offset;
            return jT808LocationAttachImpl0X2A;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x2A value, IFormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.IOStatus);
            return offset;
        }
    }
}
