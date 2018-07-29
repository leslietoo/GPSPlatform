using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using MessagePack;
using System.Collections.Generic;

namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 定位数据批量上传
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0704Formatter))]
    public class JT808_0x0704 : JT808Bodies
    {
        /// <summary>
        /// 数据项个数
        /// </summary>
        [Key(0)]
        public ushort Count { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        [Key(1)]
        public BatchLocationType LocationType { get; set; }

        /// <summary>
        /// 位置汇报数据集合
        /// </summary>
        [Key(2)]
        public IList<JT808_0x0200> Positions { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        public enum BatchLocationType:byte
        {
            正常位置批量汇报=0x00,
            盲区补报=0x01
        }
    }
}
