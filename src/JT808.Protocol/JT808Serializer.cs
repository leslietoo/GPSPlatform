using JT808.Protocol.Attributes;
using JT808.Protocol.Exceptions;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Resolvers;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol
{
    public static class JT808Serializer
    {
        static IJT808FormatterResolver defaultResolver;

        static JT808Serializer()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static IJT808FormatterResolver DefaultResolver
        {
            get
            {
                if (defaultResolver == null)
                {
                    defaultResolver = JT808StandardResolver.Instance;
                }
                return defaultResolver;
            }
        }

        public static bool IsInitialized
        {
            get
            {
               // System.Buffers.Binary.BinaryPrimitives.ReadInt16BigEndian()
                return defaultResolver != null;
            }
        }

        public static byte[] Serialize(JT808Package jT808Package)
        {
            return Serialize(jT808Package, defaultResolver);
        }

        public static byte[] Serialize(JT808Package jT808Package, IJT808FormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatter<JT808Package>();
            // ref https://www.cnblogs.com/TianFang/p/9193881.html
            var pool = ArrayPool<byte>.Shared;
            byte[] buffer = pool.Rent(65536);
            try
            {
                var len = formatter.Serialize(ref buffer, 0, jT808Package, DefaultResolver);
                return buffer.AsSpan().Slice(0, len).ToArray();
            }
            catch (JT808Exception ex)
            {
                throw new JT808Exception("Serialize", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Serialize", ex);
            }
            finally
            {
                pool.Return(buffer, true);
            }
        }

        public static JT808Package Deserialize(ReadOnlySpan<byte> bytes, IJT808FormatterResolver resolver)
        {
            try
            {
                if (resolver == null) resolver = DefaultResolver;
                var formatter = resolver.GetFormatter<JT808Package>();
                int readSize;
                return formatter.Deserialize(bytes, 0, resolver, out readSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Deserialize", ex);
            }
        }

        public static JT808Package Deserialize(ReadOnlySpan<byte> bytes)
        {
            return Deserialize(bytes, defaultResolver);
        }

        public static byte[] Serialize<T>(T obj)
        {
            return Serialize(obj, defaultResolver);
        }

        public static byte[] Serialize<T>(T obj, IJT808FormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatter<T>();
            // ref https://www.cnblogs.com/TianFang/p/9193881.html
            var pool = ArrayPool<byte>.Shared;
            byte[] buffer = pool.Rent(65536);
            try
            {
                var len = formatter.Serialize(ref buffer, 0, obj, DefaultResolver);
                return buffer.AsSpan().Slice(0, len).ToArray();
            }
            catch (JT808Exception ex)
            {
                throw new JT808Exception("Serialize", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Serialize", ex);
            }
            finally
            {
                pool.Return(buffer, true);
            }
        }

        public static T Deserialize<T>(ReadOnlySpan<byte> bytes)
        {
            return Deserialize<T>(bytes, defaultResolver);
        }

        public static T Deserialize<T>(ReadOnlySpan<byte> bytes, IJT808FormatterResolver resolver)
        {
            try
            {
                if (resolver == null) resolver = DefaultResolver;
                var formatter = resolver.GetFormatter<T>();
                int readSize;
                return formatter.Deserialize(bytes, 0, resolver, out readSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Deserialize", ex);
            }
        }
    }
}
