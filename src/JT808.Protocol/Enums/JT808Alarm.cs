using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Enums
{
    /// <summary>
    /// 报警标志
    /// </summary>
    public enum JT808Alarm
    {
        /// <summary>
        /// 紧急报警_触动报警开关后触发
        /// 收到应答后清零
        /// </summary>
        紧急报警_触动报警开关后触发 = 0,
        /// <summary>
        /// 超速报警
        /// 标志维持至报警条件解除
        /// </summary>
        超速报警 = 1,
        /// <summary>
        /// 标志维持至报警条件解除
        /// 疲劳驾驶
        /// </summary>
        疲劳驾驶 = 2,
        /// <summary>
        /// 危险预警
        /// 收到应答后清零
        /// </summary>
        危险预警 = 3,
        /// <summary>
        /// GNSS模块发生故障
        /// 标志维持至报警条件解除
        /// </summary>
        GNSS模块发生故障=4,
        /// <summary>
        /// GNSS天线未接或被剪断
        /// 标志维持至报警条件解除
        /// </summary>
        GNSS天线未接或被剪断 = 5,
        /// <summary>
        /// GNSS天线短路
        /// 标志维持至报警条件解除
        /// </summary>
        GNSS天线短路 = 6,
        /// <summary>
        /// 终端主电源欠压
        /// 标志维持至报警条件解除
        /// </summary>
        终端主电源欠压 = 7,
        /// <summary>
        /// 终端主电源掉电
        /// 标志维持至报警条件解除
        /// </summary>
        终端主电源掉电 = 8,
        /// <summary>
        /// 终端LCD或显示器故障
        /// 标志维持至报警条件解除
        /// </summary>
        终端LCD或显示器故障 = 9,
        /// <summary>
        /// TTS模块故障
        /// 标志维持至报警条件解除
        /// </summary>
        TTS模块故障 = 10,
        /// <summary>
        /// 摄像头故障
        /// 标志维持至报警条件解除
        /// </summary>
        摄像头故障 = 11,
        /// <summary>
        /// 道路运输证IC卡模块故障
        /// 标志维持至报警条件解除
        /// </summary>
        道路运输证IC卡模块故障 = 12,
        /// <summary>
        /// 超速预警
        /// 标志维持至报警条件解除
        /// </summary>
        超速预警 = 13,
        /// <summary>
        /// 疲劳驾驶预警
        /// 标志维持至报警条件解除
        /// </summary>
        疲劳驾驶预警 = 14,
        保留1=15,
        保留2=16,
        保留3=17,
        /// <summary>
        /// 当天累计驾驶超时
        /// 标志维持至报警条件解除
        /// </summary>
        当天累计驾驶超时 = 18,
        /// <summary>
        /// 超时停车
        /// 标志维持至报警条件解除
        /// </summary>
        超时停车 = 19,
        /// <summary>
        /// 进出区域
        /// 收到应答后清零
        /// </summary>
        进出区域 = 20,
        /// <summary>
        /// 进出路线
        /// 收到应答后清零
        /// </summary>
        进出路线 = 21,
        /// <summary>
        /// 路段行驶时间不足或过长
        /// 收到应答后清零
        /// </summary>
        路段行驶时间不足或过长=22,
        /// <summary>
        /// 路线偏离报警
        /// 标志维持至报警条件解除
        /// </summary>
        路线偏离报警 = 23,
        /// <summary>
        /// 车辆VSS故障
        /// 标志维持至报警条件解除
        /// </summary>
        车辆VSS故障=24,
        /// <summary>
        /// 车辆油量异常
        /// 标志维持至报警条件解除
        /// </summary>
        车辆油量异常 = 25,
        /// <summary>
        /// 车辆被盗通过车辆防盗器
        /// 标志维持至报警条件解除
        /// </summary>
        车辆被盗 = 26,
        /// <summary>
        /// 车辆非法点火
        /// </summary>
        车辆非法点火 = 27,
        /// <summary>
        /// 车辆非法位移
        /// 收到应答后清零
        /// </summary>
        车辆非法位移 = 28,
        /// <summary>
        /// 碰撞预警
        /// 标志维持至报警条件解除
        /// </summary>
        碰撞预警 = 29,
        /// <summary>
        /// 侧翻预警
        /// 标志维持至报警条件解除
        /// </summary>
        侧翻预警 = 30,
        /// <summary>
        /// 非法开门报警
        /// （终端未设置区域时，不判断非法开门）
        /// 收到应答后清零
        /// </summary>
        非法开门报警 = 31
    }
}
