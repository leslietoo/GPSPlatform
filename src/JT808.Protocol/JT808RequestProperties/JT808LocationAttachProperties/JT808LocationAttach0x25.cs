using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x25 : IJT808Properties
    {
        /// <summary>
        /// 扩展车辆信号状态位
        /// </summary>
        public int CarSignalStatus { get; set; }
    }
}
