using System;
using MessagePack;
using JT808.Protocol.JT808Formatters;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808数据包
    /// </summary>
    [Serializable]
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808PackageFromatter))]
    public class JT808Package
    {
        /// <summary>
        /// 起始符
        /// </summary>
        [IgnoreMember]
        public const byte BeginFlag = 0x7e;
        /// <summary>
        /// 终止符
        /// </summary>
        [IgnoreMember]
        public const byte EndFlag = 0x7e;

        /// <summary>
        /// 起始符
        /// </summary>
        [Key(0)]
        public byte Begin { get; set; }=  BeginFlag;

        /// <summary>
        /// 起始符
        /// </summary>
        [Key(4)]
        public byte End { get; set; } = EndFlag;

        /// <summary>
        /// 校验码
        /// 从消息头开始，同后一字节异或，直到校验码前一个字节，占用一个字节。
        /// </summary>
        [Key(3)]
        public byte CheckCode { get;  set; }

        /// <summary>
        /// 头数据
        /// </summary>
        [Key(1)]
        public JT808Header Header { get;  set; }

        /// <summary>
        /// 数据体
        /// </summary>
        [Key(2)]
        public JT808Bodies Bodies { get;  set; }
    }
}
