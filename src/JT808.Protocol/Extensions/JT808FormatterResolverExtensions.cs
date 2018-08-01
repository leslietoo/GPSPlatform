using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Extensions
{
    public static class JT808FormatterResolverExtensions
    {
        delegate int SerializeMethod(object dynamicFormatter, ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver);

        delegate dynamic DeserializeMethod(object dynamicFormatter, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize);

        static readonly ConcurrentDictionary<Type,(object Value, SerializeMethod SerializeMethod)> serializers = new ConcurrentDictionary<Type, (object Value, SerializeMethod SerializeMethod)>();

        static readonly ConcurrentDictionary<Type, (object Value, DeserializeMethod DeserializeMethod)> deserializes = new ConcurrentDictionary<Type, (object Value, DeserializeMethod DeserializeMethod)>();

        //int Serialize(ref byte[] bytes, int offset, T value, IFormatterResolver formatterResolver);
        //T Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize);

        /// <summary>
        /// 
        /// ref:MessagePack.Formatters.DynamicObjectTypeFallbackFormatter
        /// </summary>
        /// <param name="objFormatter"></param>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="bodiesImplValue"></param>
        /// <param name="formatterResolver"></param>
        /// <returns></returns>
        public static int JT808DynamicSerialize(object objFormatter, ref byte[] bytes, int offset,dynamic value ,IFormatterResolver formatterResolver)
        {
            Type type =value.GetType();
            var ti = type.GetTypeInfo();
            (object Value, SerializeMethod SerializeMethod) formatterAndDelegate;
            if (!serializers.TryGetValue(type, out formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IMessagePackFormatter<>).MakeGenericType(t);
                    var param0 = Expression.Parameter(typeof(object), "formatter");
                    var param1 = Expression.Parameter(typeof(byte[]).MakeByRefType(), "bytes");
                    var param2 = Expression.Parameter(typeof(int), "offset");
                    var param3 = Expression.Parameter(typeof(object), "value");
                    var param4 = Expression.Parameter(typeof(IFormatterResolver), "formatterResolver");
                    var serializeMethodInfo = formatterType.GetRuntimeMethod("Serialize", new[] { typeof(byte[]).MakeByRefType(), typeof(int), t, typeof(IFormatterResolver) });
                    var body = Expression.Call(
                        Expression.Convert(param0, formatterType),
                        serializeMethodInfo,
                        param1,
                        param2,
                        ti.IsValueType ? Expression.Unbox(param3, t) : Expression.Convert(param3, t),
                        param4);
                    var lambda = Expression.Lambda<SerializeMethod>(body, param0, param1, param2, param3, param4).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                serializers.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.SerializeMethod(formatterAndDelegate.Value, ref bytes, offset, value, formatterResolver);
        }

        /// <summary>
        /// ref:MessagePack.Formatters.DynamicObjectTypeFallbackFormatter
        /// </summary>
        /// <param name="objFormatter"></param>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="formatterResolver"></param>
        /// <param name="readSize"></param>
        /// <returns></returns>
        public static dynamic JT808DynamicDeserialize(object objFormatter, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            var type = objFormatter.GetType();
            (object Value, DeserializeMethod DeserializeMethod) formatterAndDelegate;
            if (!deserializes.TryGetValue(type, out formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IMessagePackFormatter<>).MakeGenericType(t);
                    ParameterExpression param0 = Expression.Parameter(typeof(object), "formatter");
                    ParameterExpression param1 = Expression.Parameter(typeof(byte[]), "bytes");
                    ParameterExpression param2 = Expression.Parameter(typeof(int), "offset");
                    ParameterExpression param3 = Expression.Parameter(typeof(IFormatterResolver), "formatterResolver");
                    ParameterExpression param4 = Expression.Parameter(typeof(int).MakeByRefType(), "readSize");
                    var deserializeMethodInfo = type.GetRuntimeMethod("Deserialize", new[] { typeof(byte[]), typeof(int), typeof(IFormatterResolver), typeof(int).MakeByRefType() });
                    var body = Expression.Call(
                        Expression.Convert(param0, type),
                        deserializeMethodInfo,
                        param1,
                        param2,
                        param3,
                        param4);
                    var lambda = Expression.Lambda<DeserializeMethod>(body, param0, param1, param2, param3, param4).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                deserializes.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.DeserializeMethod(formatterAndDelegate.Value, bytes, offset, formatterResolver, out readSize);
        }
    }
}
