using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters;
using MessagePack;

namespace JT808.Protocol
{
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808HeaderMessageBodyPropertyFormatter))]
    [JT808Formatter(typeof(JT808HeaderMessageBodyPropertyFormatter))]
    public class JT808HeaderMessageBodyProperty
    {
        public JT808HeaderMessageBodyProperty()
        {
            IsPackge = false;
            Encrypt = JT808EncryptMethod.None;
            PackgeCount = 0;
            PackageIndex = 0;
        }

        /// <summary>
        /// 是否分包
        ///  true-1  表示消息体为长消息，进行分包发送处理
        ///  false-0 消息头中无消息包封装项字段。
        /// </summary>
        [IgnoreMember]
        public bool IsPackge { get; set; }
        /// <summary>
        /// 加密标识，0为不加密
        /// 当此三位都为 0，表示消息体不加密；
        /// 当第 10 位为 1，表示消息体经过 RSA 算法加密；
        /// </summary>
        [IgnoreMember]
        public JT808EncryptMethod Encrypt { get; set; }
        /// <summary>
        /// 消息体长度
        /// </summary>
        [IgnoreMember]
        public int DataLength { get; set; }
        /// <summary>
        /// 消息总包数
        /// </summary>
        [IgnoreMember]
        public ushort PackgeCount { get; set; }
        /// <summary>
        /// 报序号 从1开始
        /// </summary>
        [IgnoreMember]
        public ushort PackageIndex { get; set; }
    }
}
