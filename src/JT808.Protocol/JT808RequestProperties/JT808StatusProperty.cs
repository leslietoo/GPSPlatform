using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties
{
    /// <summary>
    /// 状态位
    /// </summary>
    public class JT808StatusProperty
    {
        private const int bitCount = 32;

        /// <summary>
        /// 初始化读取状态位
        /// </summary>
        /// <param name="alarmStr"></param>
        public JT808StatusProperty(string alarmStr)
        {
            ReadOnlySpan<char> span = alarmStr.AsSpan();
            for (int i = 0; i < span.Length; i++)
            {
                this.GetType().GetProperty("Bit" + i.ToString()).SetValue(this, span[i]);
            }
        }

        /// <summary>
        /// 写入状态位
        /// 从左开始写入，不满32位自动补'0'
        /// </summary>
        /// <param name="alarmChar"></param>
        public JT808StatusProperty(params char[] alarmChar)
        {
            if (alarmChar != null)
            {
                ReadOnlySpan<char> span = alarmChar.ToString().PadRight(bitCount, '0').AsSpan();
                for (int i = 0; i < span.Length; i++)
                {
                    this.GetType().GetProperty("Bit" + i.ToString()).SetValue(this, span[i] == '1');
                }
            }
        }

        /// <summary>
        /// 0：ACC 关；1： ACC 开
        /// </summary>
        public char Bit0 { get; set; }
        /// <summary>
        /// 0：未定位；1：定位
        /// </summary>
        public char Bit1 { get; set; }
        /// <summary>
        /// 0：北纬；1：南纬
        /// </summary>
        public char Bit2 { get; set; }
        /// <summary>
        /// 0：东经；1：西经
        /// </summary>
        public char Bit3 { get; set; }
        /// <summary>
        /// 0：运营状态；1：停运状态
        /// </summary>
        public char Bit4 { get; set; }
        /// <summary>
        /// 0：经纬度未经保密插件加密；1：经纬度已经保密插件加密
        /// </summary>
        public char Bit5 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit6 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit7 { get; set; }
        /// <summary>
        /// 00：空车；01：半载；10：保留；11：满载（可用于客车的空、重车及货车的空载、满载状态表示，人工输入或传感器获取）
        /// </summary>
        public char Bit8 { get; set; }
        /// <summary>
        /// 00：空车；01：半载；10：保留；11：满载（可用于客车的空、重车及货车的空载、满载状态表示，人工输入或传感器获取）
        /// </summary>
        public char Bit9 { get; set; }
        /// <summary>
        /// 0：车辆油路正常；1：车辆油路断开
        /// </summary>
        public char Bit10 { get; set; }
        /// <summary>
        /// 0：车辆电路正常；1：车辆电路断开
        /// </summary>
        public char Bit11 { get; set; }
        /// <summary>
        /// 0：车门解锁；1：车门加锁
        /// </summary>
        public char Bit12 { get; set; }
        /// <summary>
        /// 0：门 1 关；1：门 1 开（前门）
        /// </summary>
        public char Bit13 { get; set; }
        /// <summary>
        /// 0：门 2 关；1：门 2 开（中门）
        /// </summary>
        public char Bit14 { get; set; }
        /// <summary>
        /// 0：门 3 关；1：门 3 开（后门）
        /// </summary>
        public char Bit15 { get; set; }
        /// <summary>
        /// 0：门 4 关；1：门 4 开（驾驶席门）
        /// </summary>
        public char Bit16 { get; set; }
        /// <summary>
        /// 0：门 5 关；1：门 5 开（自定义）
        /// </summary>
        public char Bit17 { get; set; }
        /// <summary>
        /// 0：未使用 GPS 卫星进行定位；1：使用 GPS 卫星进行定位
        /// </summary>
        public char Bit18 { get; set; }
        /// <summary>
        /// 0：未使用北斗卫星进行定位；1：使用北斗卫星进行定位
        /// </summary>
        public char Bit19 { get; set; }
        /// <summary>
        /// 0：未使用 GLONASS 卫星进行定位；1：使用 GLONASS 卫星进行定位
        /// </summary>
        public char Bit20 { get; set; }
        /// <summary>
        /// 0：未使用 Galileo 卫星进行定位；1：使用 Galileo 卫星进行定位
        /// </summary>
        public char Bit21 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit22 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit23 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit24 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit25 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit26 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit27 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit28 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit29 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit30 { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public char Bit31 { get; set; }

        /// <summary>
        /// 状态位
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Span<char> span = new char[bitCount];
            for (int i = 0; i < span.Length; i++)
            {
                span[i] = (char)this.GetType().GetProperty("Bit" + i.ToString()).GetValue(this);
            }
            return span.ToString();
        }
    }
}
