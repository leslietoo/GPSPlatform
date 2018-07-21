using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x03: IJT808Properties
    {
        /// <summary>
        /// 行驶记录功能获取的速度
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 行驶记录功能获取的速度 1/10km/h
        /// </summary>
        public double ConvertSpeed => Speed / 10.0;
    }
}
