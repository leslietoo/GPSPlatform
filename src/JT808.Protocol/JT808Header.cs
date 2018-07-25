using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.JT808Formatters;
using MessagePack;
using Protocol.Common.Extensions;
using System;
using System.Text;

namespace JT808.Protocol
{
    /// <summary>
    /// 头部
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808HeaderFormatter))]
    public class JT808Header : JT808BufferedEntityBase
    {
        public JT808Header(Memory<byte> buffer) : base(buffer)
        {
        }

        public JT808Header():base()
        {
           
        }

        /// <summary>
        /// 消息ID 
        /// </summary>
        [Key(0)]
        public JT808MsgId MsgId { get; set; }
        [Key(1)]
        [MessagePackFormatter(typeof(JT808MessageBodyPropertyFormatter))]
        public JT808MessageBodyProperty MessageBodyProperty { get; set; }
        /// <summary>
        /// 终端手机号
        /// 根据安装后终端自身的手机号转换。手机号不足 12 位，则在前补充数字，大陆手机号补充数字 0，港澳台则根据其区号进行位数补充
        /// </summary>
        [Key(2)]
        public string TerminalPhoneNo { get;  set; }
        /// <summary>
        /// 消息流水号
        /// 发送计数器
        /// 占用四个字节，为发送信息的序列号，用于接收方检测是否有信息的丢失，上级平台和下级平台接自己发送数据包的个数计数，互不影响。
        /// 程序开始运行时等于零，发送第一帧数据时开始计数，到最大数后自动归零
        /// </summary>
        [Key(3)]
        public ushort MsgNum { get;  set; }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            try
            {
                Span<byte> span = new byte[2 + 2 + 6 + 2 + 1];
                // 1.消息ID
                span.WriteLittle((int)MsgId, 0, 2);
                // 2.消息体属性
                span.WriteLittle(Write(MessageBodyProperty), 2, 2);
                // 3.终端手机号 (写死大陆手机号码)
                span.WriteBCDLittle(TerminalPhoneNo, 4, 6);
                // 4.消息流水号
                span.WriteLittle(MsgNum, 10, 2);
                // 5.写入buffer
                Buffer = span.ToArray();
            }
            catch (Exception ex)
            {
                throw new JT808Exception($"{nameof(JT808Header)}.{nameof(WriteBuffer)}", ex);
            }
        }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            try
            {
                // 1.消息ID
                MsgId = (JT808MsgId)Buffer.Span.ReadIntH2L(0, 2);
                // 2.消息体属性
                ReadOnlySpan<char> msgMethod = Convert.ToString(Buffer.Span.ReadIntH2L(2, 2), 2).PadLeft(16, '0').AsSpan();
                MessageBodyProperty = Read((ushort)Buffer.Span.ReadIntH2L(2, 2));
                // 3.终端手机号 (写死大陆手机号码)
                TerminalPhoneNo = Buffer.Span.ReadBCD(4, 6).ToString().PadLeft(12, '0');
                // 4.消息流水号
                MsgNum = (ushort)Buffer.Span.ReadIntH2L(10, 2);
                if (!MessageBodyProperty.IsPackge)
                {
                    Buffer = Buffer.Slice(0, 12);
                }
            }
            catch (Exception ex)
            {
                throw new JT808Exception($"{nameof(JT808Header)}.{nameof(ReadBuffer)}", ex);
            }
        }

        public JT808MessageBodyProperty Read(ushort property)
        {
            JT808MessageBodyProperty messageBodyProperty = new JT808MessageBodyProperty();
            ReadOnlySpan<char> msgMethod = Convert.ToString(property, 2).PadLeft(16, '0').AsSpan();
            messageBodyProperty.DataLength = Convert.ToInt32(msgMethod.Slice(6, 10).ToString(), 2);
            //  2.2. 数据加密方式
            switch (msgMethod.Slice(3, 3).ToString())
            {
                case "000":
                    messageBodyProperty.Encrypt = JT808EncryptMethod.None;
                    break;
                case "001":
                    messageBodyProperty.Encrypt = JT808EncryptMethod.RSA;
                    break;
                default:
                    messageBodyProperty.Encrypt = JT808EncryptMethod.None;
                    break;
            }
            messageBodyProperty.IsPackge = msgMethod[2] != '0';
            messageBodyProperty.PackgeCount = 0;
            messageBodyProperty.PackageIndex = 0;
            return messageBodyProperty;
        }

        public ushort Write(JT808MessageBodyProperty messageBodyProperty)
        {
            // 2.消息体属性
            Span<char> msgMethod = new char[16];
            //  2.1.保留
            msgMethod[0] = '0';
            msgMethod[1] = '0';
            //  2.2.是否分包
            msgMethod[2] = messageBodyProperty.IsPackge ? '1' : '0';
            //  2.3.数据加密方式
            switch (messageBodyProperty.Encrypt)
            {
                case JT808EncryptMethod.None:
                    msgMethod[3] = '0';
                    msgMethod[4] = '0';
                    msgMethod[5] = '0';
                    break;
                case JT808EncryptMethod.RSA:
                    msgMethod[3] = '0';
                    msgMethod[4] = '0';
                    msgMethod[5] = '1';
                    break;
                default:
                    msgMethod[3] = '0';
                    msgMethod[4] = '0';
                    msgMethod[5] = '0';
                    break;
            }
            //  2.4.数据长度
            ReadOnlySpan<char> dataLen = Convert.ToString(messageBodyProperty.DataLength, 2).PadLeft(10, '0').AsSpan();
            for (int i = 1; i <= 10; i++)
            {
                msgMethod[5 + i] = dataLen[i - 1];
            }
            return Convert.ToUInt16(msgMethod.ToString(), 2);
        }
    }
}
