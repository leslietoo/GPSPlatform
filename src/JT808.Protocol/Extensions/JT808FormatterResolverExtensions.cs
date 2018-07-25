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
        public static int JT808DynamicSerialize(this object objFormatter, ref byte[] bytes, int offset,dynamic bodiesImpl ,IFormatterResolver formatterResolver)
        {

            var methodInfo = objFormatter.GetType().GetMethod("Serialize");
            offset +=(int) methodInfo.Invoke(objFormatter,new object[] { bytes, offset, bodiesImpl, formatterResolver });
            return offset;
        }

        //public static dynamic JT808DynamicDeserialize(this object objFormatter, Type type)
        //{
        //    var methodInfo = typeof(IFormatterResolver).GetRuntimeMethod("GetFormatter", Type.EmptyTypes);
        //    dynamic formatter = methodInfo.MakeGenericMethod(type).Invoke(resolver, null);
        //    return formatter;
        //}
    }
}
