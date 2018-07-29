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

        static JT808PackageBase()
        {
            JT808GlobalConfigs.RegisterMessagePackFormatter();
        }
    }
}
