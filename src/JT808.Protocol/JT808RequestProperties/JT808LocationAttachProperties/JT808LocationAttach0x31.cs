using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x31 : IJT808Properties
    {
        /// <summary>
        /// GNSS 定位卫星数
        /// </summary>
        public byte GNSSCount { get; set; }
    }
}
