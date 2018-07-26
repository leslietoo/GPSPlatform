using System;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200_0x01Formatter))]
    public class JT808LocationAttachImpl0x01 : JT808LocationAttachBase
    {
        [Key(0)]
        public override byte AttachInfoId { get;  set; } = 0x01;
        [Key(1)]
        public override byte AttachInfoLength { get;  set; } = 4;
        /// <summary>
        /// 里程
        /// </summary>
        [Key(2)]
        public int Mileage { get; set; }
        /// <summary>
        /// 里程 1/10km，对应车上里程表读数
        /// </summary>
        [IgnoreMember]
        public double ConvertMileage => Mileage / 10;
    }
}
