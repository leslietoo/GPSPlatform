using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters;

namespace JT808.Protocol.JT808Resolvers
{
    public class JT808StandardResolver : IJT808FormatterResolver
    {
        public static readonly IJT808FormatterResolver Instance = new JT808StandardResolver();

        JT808StandardResolver()
        {
        }

        public IJT808Formatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJT808Formatter<T> formatter;

            static FormatterCache()
            {
                var attr = typeof(T).GetTypeInfo().GetCustomAttribute<JT808FormatterAttribute>();
                if (attr == null)
                {
                    return;
                }
                if (attr.Arguments == null)
                {
                    formatter = (IJT808Formatter<T>)Activator.CreateInstance(attr.FormatterType);
                }
                else
                {
                    formatter = (IJT808Formatter<T>)Activator.CreateInstance(attr.FormatterType, attr.Arguments);
                }
            }
        }
    }
}
