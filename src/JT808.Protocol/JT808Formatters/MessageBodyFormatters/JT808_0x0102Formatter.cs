using JT808.Protocol.MessageBodyRequest;
using MessagePack;
using MessagePack.Formatters;
using JT808.Protocol.Extensions;


namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0102Formatter : IMessagePackFormatter<JT808_0x0102>
    {
        public JT808_0x0102 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0102 jT808_0X0102 = new JT808_0x0102();
            jT808_0X0102.Code = JT808BinaryExtensions.ReadStringLittle(bytes, offset);
            offset= jT808_0X0102.Code.Length;
            readSize = offset;
            return jT808_0X0102;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0102 value, IFormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.Code);
            return offset;
        }
    }
}
