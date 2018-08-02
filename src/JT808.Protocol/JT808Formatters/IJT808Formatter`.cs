using System;

namespace JT808.Protocol.JT808Formatters
{
    public interface IJT808Formatter<T>: IJT808Formatter
    {
        T Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize);

        int Serialize(Span<byte> bytes, int offset, T value, IJT808FormatterResolver formatterResolver);
    }
}
