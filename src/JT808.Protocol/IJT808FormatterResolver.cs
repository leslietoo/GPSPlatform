using JT808.Protocol.JT808Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol
{
    public interface IJT808FormatterResolver
    {
        IJT808Formatter<T> GetFormatter<T>();
    }
}
