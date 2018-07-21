using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x13 : IJT808Properties
    {
        /// <summary>
        /// 路段 ID
        /// </summary>
        public int DrivenRouteId { get; set; }

        /// <summary>
        /// 路段行驶时间
        /// 单位为秒（s)
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        ///  结果 0：不足；1：过长
        /// </summary>
        public JT808DrivenRouteType DrivenRoute { get; set; }
    }
}
