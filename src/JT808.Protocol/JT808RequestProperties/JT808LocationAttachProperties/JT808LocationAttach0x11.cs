using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x11 : IJT808Properties
    {
        /// <summary>
        /// 超速报警附加信息
        /// 0：无特定位置；
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// 若位置类型为 0，无该字段
        /// </summary>
        public int AreaId { get; set; }
    }
}
