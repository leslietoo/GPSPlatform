using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808RequestProperties.JT808LocationAttachProperties
{
    public class JT808LocationAttach0x12 : IJT808Properties
    {
        /// <summary>
        /// 位置类型
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT808PositionType JT808PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 方向 
        /// 0：进
        /// 1：出
        /// </summary>
        public JT808DirectionType Direction { get; set; }
    }
}
