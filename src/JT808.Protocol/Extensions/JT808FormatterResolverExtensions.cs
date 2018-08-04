using JT808.Protocol.JT808Formatters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Extensions
{
    /// <summary>
    /// 
    /// ref http://adamsitnik.com/Span/#span-must-not-be-a-generic-type-argument
    /// ref http://adamsitnik.com/Span/
    /// </summary>
    public static class JT808FormatterResolverExtensions
    {
        delegate int JT808SerializeMethod(object dynamicFormatter, ref byte[] bytes, int offset, object value, IJT808FormatterResolver formatterResolver);

        delegate dynamic JT808DeserializeMethod(object dynamicFormatter, ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize);

        static readonly ConcurrentDictionary<Type, (object Value, JT808SerializeMethod SerializeMethod)> jT808Serializers = new ConcurrentDictionary<Type, (object Value, JT808SerializeMethod SerializeMethod)>();

        static readonly ConcurrentDictionary<Type, (object Value, JT808DeserializeMethod DeserializeMethod)> jT808Deserializes = new ConcurrentDictionary<Type, (object Value, JT808DeserializeMethod DeserializeMethod)>();

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
        public static int JT808DynamicSerialize(object objFormatter, ref byte[] bytes, int offset, dynamic value, IJT808FormatterResolver formatterResolver)
        {
            Type type = value.GetType();
            var ti = type.GetTypeInfo();
            (object Value, JT808SerializeMethod SerializeMethod) formatterAndDelegate;
            if (!jT808Serializers.TryGetValue(type, out formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IJT808Formatter<>).MakeGenericType(t);
                    var param0 = Expression.Parameter(typeof(object), "formatter");
                    var param1 = Expression.Parameter(typeof(byte[]).MakeByRefType(), "bytes");
                    var param2 = Expression.Parameter(typeof(int), "offset");
                    var param3 = Expression.Parameter(typeof(object), "value");
                    var param4 = Expression.Parameter(typeof(IJT808FormatterResolver), "formatterResolver");
                    var serializeMethodInfo = formatterType.GetRuntimeMethod("Serialize", new[] { typeof(byte[]).MakeByRefType(), typeof(int), t, typeof(IJT808FormatterResolver) });
                    var body = Expression.Call(
                        Expression.Convert(param0, formatterType),
                        serializeMethodInfo,
                        param1,
                        param2,
                        ti.IsValueType ? Expression.Unbox(param3, t) : Expression.Convert(param3, t),
                        param4);
                    var lambda = Expression.Lambda<JT808SerializeMethod>(body, param0, param1, param2, param3, param4).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                jT808Serializers.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.SerializeMethod(formatterAndDelegate.Value,ref bytes, offset, value, formatterResolver);
        }

        /// <summary>
        /// 
        /// ref:MessagePack.Formatters.DynamicObjectTypeFallbackFormatter
        /// </summary>
        /// <param name="objFormatter"></param>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="formatterResolver"></param>
        /// <param name="readSize"></param>
        /// <returns></returns>
        public static dynamic JT808DynamicDeserialize(object objFormatter,ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            var type = objFormatter.GetType();
            (object Value, JT808DeserializeMethod DeserializeMethod) formatterAndDelegate;
            if (!jT808Deserializes.TryGetValue(type, out formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(IJT808Formatter<>).MakeGenericType(t);
                    ParameterExpression param0 = Expression.Parameter(typeof(object), "formatter");
                    ParameterExpression param1 = Expression.Parameter(typeof(ReadOnlySpan<byte>), "bytes");
                    ParameterExpression param2 = Expression.Parameter(typeof(int), "offset");
                    ParameterExpression param3 = Expression.Parameter(typeof(IJT808FormatterResolver), "formatterResolver");
                    ParameterExpression param4 = Expression.Parameter(typeof(int).MakeByRefType(), "readSize");
                    var deserializeMethodInfo = type.GetRuntimeMethod("Deserialize", new[] { typeof(ReadOnlySpan<byte>), typeof(int), typeof(IJT808FormatterResolver), typeof(int).MakeByRefType() });
                    var body = Expression.Call(
                        Expression.Convert(param0, type),
                        deserializeMethodInfo,
                        param1,
                        param2,
                        param3,
                        param4);
                    var lambda = Expression.Lambda<JT808DeserializeMethod>(body, param0, param1, param2, param3, param4).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                jT808Deserializes.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.DeserializeMethod(formatterAndDelegate.Value, bytes, offset, formatterResolver, out readSize);
        }

        public static object GetFormatterDynamic(this IJT808FormatterResolver resolver, Type type)
        {
            var methodInfo = typeof(IJT808FormatterResolver).GetRuntimeMethod("GetFormatter", Type.EmptyTypes);
            var formatter = methodInfo.MakeGenericMethod(type).Invoke(resolver, null);
            return formatter;
        }
    }
}
