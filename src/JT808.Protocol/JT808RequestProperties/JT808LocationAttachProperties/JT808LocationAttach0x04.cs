using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x04 : IJT808Properties
    {
        /// <summary>
        /// 需要人工确认报警事件的 ID，从 1 开始计数
        /// </summary>
        public int EventId { get; set; }
    }
}
