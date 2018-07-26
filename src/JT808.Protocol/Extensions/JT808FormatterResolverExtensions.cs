using MessagePack;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Extensions
{
    public static class JT808FormatterResolverExtensions
    {
        //int Serialize(ref byte[] bytes, int offset, T value, IFormatterResolver formatterResolver);
        //T Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize);
        public static int JT808DynamicSerialize(object objFormatter, ref byte[] bytes, int offset,dynamic bodiesImpl ,IFormatterResolver formatterResolver)
        {
            var methodInfo = objFormatter.GetType().GetMethod("Serialize");
            return (int)methodInfo.Invoke(objFormatter, new object[] { bytes, offset, bodiesImpl, formatterResolver }); ;
        }

        public static dynamic JT808DynamicDeserialize(object objFormatter, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var methodInfo = objFormatter.GetType().GetMethod("Deserialize");
            readSize = 0;
            var bodiesImpl= methodInfo.Invoke(objFormatter, new object[] { bytes, offset, formatterResolver, readSize });
            return bodiesImpl;
        }
    }
}
