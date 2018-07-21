using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties
{
    /// <summary>
    /// 终端鉴权
    /// </summary>
    public class JT808TerminalLoginRequestProperty: IJT808Properties
    {
        /// <summary>
        /// 鉴权码
        /// </summary>
        public string Code { get; set; }
    }
}
