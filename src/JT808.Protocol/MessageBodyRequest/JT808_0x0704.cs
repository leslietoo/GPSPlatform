using System;
using System.Collections.Generic;
using System.Text;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 定位数据批量上传
    /// </summary>
    public class JT808_0x0704 : JT808Bodies
    {
        public JT808_0x0704()
        {
        }

        public JT808_0x0704(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 数据项个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 位置数据类型
        /// </summary>
        public BatchLocationType LocationType { get; set; }

        /// <summary>
        /// 位置汇报数据集合
        /// </summary>
        public List<JT808_0x0200> Positions { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Count = Buffer.Span.ReadIntH2L(0, 2);
            LocationType = (BatchLocationType)Buffer.Span[2];
            int offset = 3;
            Positions = new List<JT808_0x0200>();
            for (int i = 0; i < Count; i++)
            {
                int buflen = Buffer.Span.ReadIntH2L(offset, 2);
                try
                {
                    JT808_0x0200 jT808_0X0200 = new JT808_0x0200(Buffer.Span.Slice(offset + 2, buflen).ToArray());
                    jT808_0X0200.ReadBuffer(jT808GlobalConfigs);
                    Positions.Add(jT808_0X0200);
                }
                catch (Exception ex)
                {
                    
                }
                offset += buflen + 2;
            }
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            List<byte> bytes = new List<byte>();
            Memory<byte> memory = new byte[3];
            memory.Span.WriteLittle(Count, 0, 2);
            memory.Span.WriteLittle((byte)LocationType, 2);
            bytes.AddRange(memory.ToArray());
            if (Positions != null)
            {
                foreach(var item in Positions)
                {
                    try
                    {
                        item.WriteBuffer(jT808GlobalConfigs);
                        bytes.AddRange(item.Buffer.Length.ToBytes(2));
                        bytes.AddRange(item.Buffer.ToArray());
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            Buffer = bytes.ToArray();
        }

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
