using JT808.Protocol.Enums;
using System;


namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 多媒体数据上传
    /// </summary>
    public class JT808_0x0801 : JT808Bodies
    {
        public JT808_0x0801(Memory<byte> buffer) : base(buffer)
        {
        }

        public JT808_0x0801() : base()
        {
        }

        /// <summary>
        /// 多媒体 ID 
        /// </summary>
        public int MultimediaId { get; set; }

        /// <summary>
        /// 多媒体类型
        /// </summary>
        public JT808MultimediaType MultimediaType { get; set; }

        /// <summary>
        /// 多媒体格式编码
        /// </summary>
        public JT808MultimediaCodingFormat MultimediaCodingFormat { get; set; }

        /// <summary>
        /// 事件项编码
        /// </summary>
        public JT808EventItemCoding EventItemCoding { get; set; }

        /// <summary>
        /// 通道 ID
        /// </summary>
        public byte ChannelId { get; set; }

        /// <summary>
        /// 位置信息汇报(0x0200)消息体
        /// </summary>
        public JT808_0x0200 JT808_0x0200 { get; set; }

        /// <summary>
        /// 多媒体数据包
        /// </summary>
        public Memory<byte> MultimediaBuffer { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            MultimediaId = Buffer.Span.ReadIntH2L(0, 4);
            MultimediaType = (JT808MultimediaType)Buffer.Span[4];
            MultimediaCodingFormat=(JT808MultimediaCodingFormat)Buffer.Span[5];
            EventItemCoding=(JT808EventItemCoding)Buffer.Span[6];
            ChannelId = Buffer.Span[7];
            JT808_0x0200 = new JT808_0x0200(Buffer.Slice(8, 28).ToArray());
            JT808_0x0200.ReadBuffer(jT808GlobalConfigs);
            MultimediaBuffer = Buffer.Slice(36);
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer = new byte[8 + 28 + MultimediaBuffer.Length];
            Buffer.Span.WriteLittle(MultimediaId, 0, 4);
            Buffer.Span.WriteLittle((byte)MultimediaType, 4);
            Buffer.Span.WriteLittle((byte)MultimediaCodingFormat , 5);
            Buffer.Span.WriteLittle((byte)EventItemCoding, 6);
            Buffer.Span.WriteLittle(ChannelId, 7);
            JT808_0x0200.WriteBuffer(jT808GlobalConfigs);
            Buffer.Span.WriteLittle(JT808_0x0200.Buffer.Span,8);
            Buffer.Span.WriteLittle(MultimediaBuffer.Span, 36);
        }
    }
}
