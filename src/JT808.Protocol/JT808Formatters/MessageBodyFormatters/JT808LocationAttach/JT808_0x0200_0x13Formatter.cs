using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x13Formatter : IMessagePackFormatter<JT808LocationAttachImpl0x13>
    {
        public JT808LocationAttachImpl0x13 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x13 jT808LocationAttachImpl0x13 = new JT808LocationAttachImpl0x13();
            jT808LocationAttachImpl0x13.AttachInfoId = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x13.AttachInfoLength = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x13.DrivenRouteId =BinaryExtensions.ReadInt32Little(bytes, offset);
            offset = offset + 4;
            jT808LocationAttachImpl0x13.Time = BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset = offset + 2;
            jT808LocationAttachImpl0x13.DrivenRoute =(JT808DrivenRouteType)BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            readSize = offset;
            return jT808LocationAttachImpl0x13;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x13 value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.DrivenRouteId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Time);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.DrivenRoute);
            return offset;
        }
    }
}
