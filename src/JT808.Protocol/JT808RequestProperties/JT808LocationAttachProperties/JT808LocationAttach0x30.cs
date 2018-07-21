using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x30 : IJT808Properties
    {
        /// <summary>
        /// 无线通信网络信号强度
        /// </summary>
        public byte WiFiSignalStrength { get; set; }
    }
}
