using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x11Formatter : IJT808Formatter<JT808LocationAttachImpl0x11>
    {
        public JT808LocationAttachImpl0x11 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x11 jT808LocationAttachImpl0x11 = new JT808LocationAttachImpl0x11();
            jT808LocationAttachImpl0x11.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x11.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x11.JT808PositionType =(JT808PositionType)JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            if (jT808LocationAttachImpl0x11.JT808PositionType != JT808PositionType.无特定位置)
            {
                jT808LocationAttachImpl0x11.AreaId = JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            }
            readSize = offset;
            return jT808LocationAttachImpl0x11;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x11 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.JT808PositionType);
            if (value.JT808PositionType != JT808PositionType.无特定位置)
            {
                offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AreaId);
            }
            return offset;
        }
    }
}
