using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyRequest;
//using JT808.Protocol.MessageBodyReply;
using System;

namespace JT808.Protocol
{
    public static class JT808MessageBodyFactory
    {
        public static JT808Bodies Create(JT808MsgId jT808MsgId, Memory<byte> body)
        {
            switch (jT808MsgId)
            {
                case JT808MsgId.终端鉴权:
                    return new JT808_0x0102(body);
                case JT808MsgId.终端心跳:
                    return new JT808_0x0002(body);
                case JT808MsgId.平台通用应答:
                    return new JT808_0x8001(body);
                case JT808MsgId.终端注册:
                    return new JT808_0x0100(body);
                case JT808MsgId.终端注册应答:
                    return new JT808_0x8100(body);
                case JT808MsgId.终端通用应答:
                    return new JT808_0x0001(body);
                case JT808MsgId.位置信息汇报:
                    return new JT808_0x0200(body);
                case JT808MsgId.定位数据批量上传:
                    return new JT808_0x0704(body);
                case JT808MsgId.多媒体数据上传:
                     return new JT808_0x0801(body);
                default:
                    return null;
            }
        }
    }
}
