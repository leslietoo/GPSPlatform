using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties
{
    /// <summary>
    /// 位置基本信息
    /// </summary>
    public class JT808UploadLocationRequestProperty: IJT808Properties
    {
        /// <summary>
        /// 报警标志 
        /// </summary>
        public int AlarmFlag { get; set; }
        /// <summary>
        /// 状态位标志
        /// </summary>
        public int StatusFlag { get; set; }
        /// <summary>
        /// 纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// 经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// 高程
        /// 海拔高度，单位为米（m）
        /// </summary>
        public int Altitude { get; set; }
        /// <summary>
        /// 速度 1/10km/h
        /// </summary>
        public double Speed { get; set; }
        /// <summary>
        /// 方向 0-359，正北为 0，顺时针
        /// </summary>
        public int Direction { get; set; }
        /// <summary>
        /// YY-MM-DD-hh-mm-ss（GMT+8 时间，本标准中之后涉及的时间均采用此时区）
        /// </summary>
        public DateTime GPSTime { get; set; }
        /// <summary>
        /// 位置附加信息
        /// </summary>
        public IDictionary<byte, IJT808Properties> JT808LocationAttachData { get; set; }
    }
}
