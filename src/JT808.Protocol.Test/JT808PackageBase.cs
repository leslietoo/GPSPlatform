using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Test
{
    public class JT808PackageBase
    {
        public readonly JT808GlobalConfigs jT808GlobalConfigs = new JT808GlobalConfigs();

        static JT808PackageBase()
        {
            MessagePack.Resolvers.CompositeResolver.RegisterAndSetAsDefault(
               new IMessagePackFormatter[]
               {
                   // for example, register reflection infos(can not serialize in default)
                   new JT808MessageBodyPropertyFormatter(),
                   new JT808PackageFromatter(),
                   new JT808HeaderFormatter(),
                   new JT808_0x0200Formatter(),
                   new JT808_0x0200_0x01Formatter(),
                   new JT808_0x0200_0x02Formatter(),
                   new JT808_0x0200_0x03Formatter(),
                   new JT808_0x0200_0x04Formatter(),
                   new JT808_0x0200_0x11Formatter(),
                   new JT808_0x0200_0x12Formatter(),
                   new JT808_0x0200_0x13Formatter(),
                   new JT808_0x0200_0x25Formatter(),
                   new JT808_0x0200_0x2AFormatter(),
                   new JT808_0x0200_0x2BFormatter(),
                   new JT808_0x0200_0x30Formatter(),
                   new JT808_0x0200_0x31Formatter(),
                   new JT808_0x0200_0x06Formatter(),
                   new JT808_0x0001Formatter(),
                   new JT808_0x8001Formatter(),
                   new JT808_0x8100Formatter(),
                   new JT808_0x0704Formatter(),
               },
               new IFormatterResolver[]
               {
                    ContractlessStandardResolver.Instance
               });
        }
    }
}
