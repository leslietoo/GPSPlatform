using System;
using System.Collections.Generic;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808RequestProperties;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 位置信息汇报
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200Formatter))]
    public class JT808_0x0200 : JT808Bodies
    {
        /// <summary>
        /// 报警标志 
        /// </summary>
        [Key(0)]
        public int AlarmFlag { get; set; }
        /// <summary>
        /// 状态位标志
        /// </summary>
        [Key(1)]
        public int StatusFlag { get; set; }
        /// <summary>
        /// 纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        [Key(2)]
        public int Lat { get; set; }
        /// <summary>
        /// 经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        [Key(3)]
        public int Lng { get; set; }
        /// <summary>
        /// 高程
        /// 海拔高度，单位为米（m）
        /// </summary>
        [Key(4)]
        public ushort Altitude { get; set; }
        /// <summary>
        /// 速度 1/10km/h
        /// </summary>
        [Key(5)]
        public ushort Speed { get; set; }
        /// <summary>
        /// 方向 0-359，正北为 0，顺时针
        /// </summary>
        [Key(6)]
        public ushort Direction { get; set; }
        /// <summary>
        /// YY-MM-DD-hh-mm-ss（GMT+8 时间，本标准中之后涉及的时间均采用此时区）
        /// </summary>
        [Key(7)]
        public DateTime GPSTime { get; set; }
        /// <summary>
        /// 位置附加信息
        /// </summary>
        [Key(8)]
        public IDictionary<byte, JT808LocationAttachBase> JT808LocationAttachData { get; set; }
    }
}
