using JT808.Protocol.Attributes;
using JT808.Protocol.MessageBodyReply;
using JT808.Protocol.MessageBodyRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// JT808消息
    /// </summary>
    public enum JT808MsgId : ushort
    {
        /// <summary>
        /// 终端通用应答
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0001))]
        终端通用应答 = 0x0001,
        /// <summary>
        /// 终端心跳
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0002))]
        终端心跳 = 0x0002,
        /// <summary>
        /// 终端注册
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0100))]
        终端注册 = 0x0100,
        /// <summary>
        /// 终端注销
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0003))]
        终端注销 = 0x0003,
        /// <summary>
        /// 终端鉴权
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0102))]
        终端鉴权 = 0x0102,
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0200))]
        位置信息汇报 = 0x0200,
        /// <summary>
        ///  终端RSA公钥【0A00】 
        /// </summary>
        终端RSA公钥 = 0x0A00,
        /// <summary>
        /// 平台通用应答
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8001))]
        平台通用应答 = 0x8001,
        /// <summary>
        /// 终端注册应答
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x8100))]
        终端注册应答 = 0x8100,
        /// <summary>
        /// 文本信息下发
        /// </summary>
        //[JT808BodiesType(typeof(JT808_0x8300))]
        文本信息下发 = 0x8300,
        /// <summary>
        /// 定位数据批量上传
        /// </summary>
        [JT808BodiesType(typeof(JT808_0x0704))]
        定位数据批量上传 = 0x0704,
        /// <summary>
        /// 多媒体数据上传
        /// </summary>
        //[JT808BodiesType(typeof(JT808_0x0801))]
        多媒体数据上传 = 0x0801,
        /// <summary>
        /// 自定义统一下发消息
        /// </summary>
        自定义统一下发消息= 0x9990
    }
}
