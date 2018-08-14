using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBodyReply
{
    /// <summary>
    /// 查询终端属性应答
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0107Formatter))]
    public class JT808_0x0107:JT808Bodies
    {
    }
}
