using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0900Formatter : IJT808Formatter<JT808_0x0900>
    {
        public JT808_0x0900 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0900 jT808_0x0900 = new JT808_0x0900();
            jT808_0x0900.PassthroughType = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            if (JT808_0x0900_BodyBase.JT808_0x0900Method.TryGetValue(jT808_0x0900.PassthroughType, out Type type))
            {
                jT808_0x0900.JT808_0x0900_BodyBase = JT808FormatterResolverExtensions.JT808DynamicDeserialize(formatterResolver.GetFormatterDynamic(type), bytes.Slice(offset), offset, formatterResolver, out readSize);
            }
            readSize = offset;
            return jT808_0x0900;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0900 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.PassthroughType);
            object obj = formatterResolver.GetFormatterDynamic(value.JT808_0x0900_BodyBase.GetType());
            offset = JT808FormatterResolverExtensions.JT808DynamicSerialize(obj, ref bytes, offset, value, formatterResolver);
            return offset;
        }
    }
}
