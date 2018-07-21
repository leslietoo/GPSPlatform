using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x2A : IJT808Properties
    {
        /// <summary>
        /// IO状态位
        /// </summary>
        public int IOStatus { get; set; }
    }
}
