using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x02: IJT808Properties
    {
        /// <summary>
        /// 油量
        /// </summary>
        public int Oil { get; set; }
        /// <summary>
        /// 油量 1/10L，对应车上油量表读数
        /// </summary>
        public double ConvertOil => Oil / 10.0;
    }
}
