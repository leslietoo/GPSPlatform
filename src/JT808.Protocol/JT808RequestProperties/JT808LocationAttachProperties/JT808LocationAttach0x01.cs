using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x01: IJT808Properties
    {
        /// <summary>
        /// 里程
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// 里程 1/10km，对应车上里程表读数
        /// </summary>
        public double ConvertMileage => Mileage / 10;
    }
}
