using System;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using Protocol.Common.Extensions;

namespace JT808.Protocol.Test.JT808LocationAttach
{
    /// <summary>
    /// 自定义附加信息
    /// Age-word-2
    /// UserName-BCD(10)
    /// Gerder-byte-1
    /// </summary>
    public class JT808LocationAttachImpl0x06 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x06()
        {
        }

        public JT808LocationAttachImpl0x06(Memory<byte> buffer) : base(buffer)
        {
        }

        public int Age { get; set; }

        public string UserName { get; set; }

        public byte Gender { get; set; }

        public override byte AttachInfoId { get; protected set; } = 0x06;

        public override byte AttachInfoLength { get; protected set; } = 13;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            UserName = Buffer.Span.ReadStringLittle(2, 10);
            Age = Buffer.Span.ReadIntH2L(12, 2);
            Gender = Buffer.Span[14];
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer= new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(UserName,2);
            Buffer.Span.WriteLittle(Age, 12, 2);
            Buffer.Span.WriteLittle(Gender, 14);
        }
    }
}
