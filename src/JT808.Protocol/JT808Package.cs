
using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using Protocol.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace JT808.Protocol
{
    /// <summary>
    /// JT808数据包
    /// </summary>
    public class JT808Package : JT808BufferedEntityBase
    {
        public JT808Package(Memory<byte> buf) : base(buf)
        {
            OriginalBuffer = buf.IsEmpty ? null : buf.ToArray();
        }

        public JT808Package() : base()
        {
        }

        public byte[] OriginalBuffer { get; }

        /// <summary>
        /// 起始符
        /// </summary>
        public const byte BeginFlag = 0x7e;
        /// <summary>
        /// 终止符
        /// </summary>
        public const byte EndFlag = 0x7e;

        /// <summary>
        /// 校验码
        /// 从消息头开始，同后一字节异或，直到校验码前一个字节，占用一个字节。
        /// </summary>
        public byte CheckCode { get;  set; }

        /// <summary>
        /// 头数据
        /// </summary>
        public JT808Header Header { get;  set; }

        /// <summary>
        /// 数据体
        /// </summary>
        public JT808Bodies Bodies { get;  set; }
      
        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Span<byte> span = new byte[1+ Header.Buffer.Length+ Bodies.Buffer.Length + 1 + 1];
            // 1.起始符
            span.WriteLittle(BeginFlag, 0);
            // 2.头数据 下发不需要分包处理
            span.WriteLittle(Header.Buffer.Span, 1);
            // 3.数据体
            span.WriteLittle(Bodies.Buffer.Span, Header.Buffer.Span.Length);
            // 4.校验码
            CheckCode = span.ToXor(1, Header.Buffer.Length + Bodies.Buffer.Length);
            span.WriteLittle(CheckCode, span.Length - 2);
            // 5.终止符
            span.WriteLittle(EndFlag, span.Length - 1);
            // 6.转义
            Buffer = JT808Escape(span).ToArray();
        }

        /// <summary>
        /// 公共写数据（适用于头部固定长度）
        /// </summary>
        /// <param name="jT808GlobalConfigs"></param>
        public void CommonWriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Header.WriteBuffer(jT808GlobalConfigs);
            try
            {
                Bodies.WriteBuffer(jT808GlobalConfigs);
            }
            catch (Exception ex)
            {
                throw new JT808Exception($"{nameof(Bodies)}.{nameof(Bodies)}", ex);
            }
            WriteBuffer(jT808GlobalConfigs);
        }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            // 转义还原——>验证校验码——>解析消息
            // 1. 解码（转义还原）
            Span<byte> buffer = JT808DeEscape(Buffer.Span,0, Buffer.Length);
            // 2. 验证校验码
            //  2.1. 获取校验位索引
            int checkIndex = buffer.Length - 2;
            //  2.2. 获取校验码
            CheckCode = buffer[checkIndex];
            //  2.3. 从消息头到校验码前一个字节
            byte checkCode = buffer.ToXor(1, checkIndex);
            //  2.4. 验证校验码
            if (CheckCode != checkCode)
            {
                throw new JT808Exception($"{CheckCode}!={checkCode}");
            }
            // 3.初始化消息头
            Header = new JT808Header(buffer.Slice(1, 12).ToArray());
            Header.ReadBuffer(jT808GlobalConfigs);
            //  3.1 判断是否分包
            if (Header.IsPackge)
            {
                Header.PackgeCount = buffer.ReadIntH2L(13, 2);
                Header.PackageIndex = buffer.ReadIntH2L(15, 2);
                try
                {
                    // 4. 分包消息体
                    Span<byte> messageBody = buffer.Slice(17, Header.DataLength);
                    // Bodies
                    Bodies = JT808MessageBodyFactory.Create(Header.MsgId, messageBody.ToArray());
                    Bodies.ReadBuffer(jT808GlobalConfigs);
                }
                catch (Exception ex)
                {
                    throw new JT808Exception($"分包消息体", ex);
                }
            }
            else
            {
                // 判断是否有消息体
                if (Header.DataLength != 0)
                {
                    try
                    {
                        // 4. 未分包消息体
                        Span<byte> messageBody = buffer.Slice(13, Header.DataLength);
                        // Bodies
                        Bodies = JT808MessageBodyFactory.Create(Header.MsgId, messageBody.ToArray());
                        Bodies.ReadBuffer(jT808GlobalConfigs);
                    }
                    catch (Exception ex)
                    {
                        throw new JT808Exception($"未分包消息体", ex);
                    }
                }
            }
        }

        /// <summary>
        /// 转码
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Span<byte> JT808DeEscape(Span<byte> buf, int offset, int length)
        {
            List<byte> bytes = new List<byte>();
            int n = 0;
            int i = offset;
            int len = offset + length;
            while (i < len)
            {
                if (buf[i] == 0x7d)
                {
                    if (len > i + 1)
                    {
                        if (buf[i + 1] == 0x01)
                        {
                            bytes.Add( 0x7d);
                            i++;
                        }
                        else if (buf[i + 1] == 0x02)
                        {
                            bytes.Add(0x7e);
                            i++;
                        }
                        else
                        {
                            bytes.Add(buf[i]);
                        }
                    }
                }
                else
                {
                    bytes.Add(buf[i]);
                }
                n++;
                i++;
            }
            return bytes.ToArray();
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static Span<byte> JT808Escape(Span<byte> buf)
        {
            List<byte> bytes = new List<byte>();
            int n = 0;
            bytes.Add(buf[0]);
            for (int i = 1; i < buf.Length - 1; i++)
            {
                if (buf[i] == BeginFlag)
                {
                    bytes.Add(0x7d);
                    bytes.Add(0x02);
                    n++;
                }
                else if (buf[i] == 0x7d)
                {
                    bytes.Add(0x7d);
                    bytes.Add(0x01);
                    n++;
                }
                else
                {
                    bytes.Add(buf[i]);
                }
            }
            if (n > 0)
            {
                bytes.Add(buf[buf.Length - 1]);
                return bytes.ToArray();
            }
            else
            {
                return buf;
            }
        }
    }
}
