using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;
using JT808.Protocol.Extensions;
using Protocol.Common.Extensions;
using JT808.Protocol.Exceptions;

namespace JT808.Protocol.JT808Formatters
{
    /// <summary>
    /// JT808包序列化器
    /// </summary>
    public class JT808PackageFromatter : IMessagePackFormatter<JT808Package>
    {
        public JT808Package Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            JT808Package jT808Package = new JT808Package();
            // 转义还原——>验证校验码——>解析消息
            // 1. 解码（转义还原）
            byte[] buffer = JT808DeEscape(bytes, 0, bytes.Length);
            // 2. 验证校验码
            //  2.1. 获取校验位索引
            int checkIndex = buffer.Length - 2;
            //  2.2. 获取校验码
            jT808Package.CheckCode = buffer[checkIndex];
            //  2.3. 从消息头到校验码前一个字节
            byte checkCode = buffer.ToXor(1, checkIndex);
            //  2.4. 验证校验码
            if (jT808Package.CheckCode != checkCode)
            {
                throw new JT808Exception($"{jT808Package.CheckCode}!={checkCode}");
            }
            jT808Package.Begin = buffer[offset];
            offset = offset + 1;
            // 3.初始化消息头
            jT808Package.Header = formatterResolver.GetFormatter<JT808Header>().Deserialize(buffer, offset, formatterResolver, out readSize);
            offset = readSize;
            Type type = JT808FormattersBodiesFactory.Create(jT808Package.Header.MsgId);
            if (type != null)
            {
                if (jT808Package.Header.MessageBodyProperty.IsPackge)
                {//4.分包消息体 从17位开始  或   未分包消息体 从13位开始
                 //消息总包数2位+包序号2位=4位
                    offset = offset + 2 + 2;
                }
                if (jT808Package.Header.MessageBodyProperty.DataLength != 0)
                {
                    try
                    {
                        //5.处理消息体
                        jT808Package.Bodies = JT808FormatterResolverExtensions.JT808DynamicDeserialize(formatterResolver.GetFormatterDynamic(type), buffer.AsSpan().Slice(offset, jT808Package.Header.MessageBodyProperty.DataLength).ToArray(), offset, formatterResolver, out readSize);
                    }
                    catch (Exception ex)
                    {
                        throw new JT808Exception($"消息体解析错误", ex);
                    }
                }
            }
            jT808Package.End= buffer[bytes.Length-1];
            readSize = buffer.Length;
            return jT808Package;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808Package value, IFormatterResolver formatterResolver)
        {
            // 1.起始符
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Begin);
            // 2.头数据 下发不需要分包处理
#warning 头部长度需要预先知道？？？？ 
            offset = formatterResolver.GetFormatter<JT808Header>().Serialize(ref bytes, offset, value.Header, formatterResolver);
            // 3.数据体
            Type type = JT808FormattersBodiesFactory.Create(value.Header.MsgId);
            if (type != null)
            {
                // 3.1 处理数据体
                offset = JT808FormatterResolverExtensions.JT808DynamicSerialize(formatterResolver.GetFormatterDynamic(type), ref bytes, offset, value.Bodies, formatterResolver);
            }
            // 4.校验码
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, bytes.AsSpan().Slice(1, offset).ToXor(0, offset));
            // 5.终止符
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.End);
            byte[] temp = JT808Escape(bytes.AsSpan().Slice(0, offset));
            Buffer.BlockCopy(temp,0, bytes, 0, temp.Length);
            return temp.Length;
        }

        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] JT808DeEscape(byte[] buf, int offset, int length)
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
                            bytes.Add(0x7d);
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
        /// 转码
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        private static byte[] JT808Escape(ReadOnlySpan<byte> buf)
        {
            List<byte> bytes = new List<byte>();
            int n = 0;
            bytes.Add(buf[0]);
            for (int i = 1; i < buf.Length - 1; i++)
            {
                if (buf[i] == 0x7e)
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
                return buf.ToArray();
            }
        }
    }
}
