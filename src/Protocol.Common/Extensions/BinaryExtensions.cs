using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Protocol.Common.Extensions
{
    public static class BinaryExtensions
    {
        /// <summary>
        /// 日期限制于2000年
        /// </summary>
        private const int DateLimitYear = 2000;

        public static int ReadIntH2L(this Span<byte> buf, int offset, int len)
        {
            int result = 0;
            for (int i = offset; i < offset + len; i++)
            {
                result += buf[i] * (int)Math.Pow(256, len + offset - i - 1);
            }
            return result;
        }

        public static long ReadBCD(this Span<byte> buf, int offset, int len)
        {
            long result = 0;
            try
            {
                for (int i = offset; i < offset + len; i++)
                {
                    result += buf[i].ReadBCD64((byte)(offset + len - i));
                }
            }
            catch
            {
            }
            return result;
        }

        public static int ReadBCD32(this byte data, byte dig)
        {
            int result = Convert.ToInt32(data.ToString("X"));
            return result * (int)Math.Pow(100, dig - 1);
        }

        public static long ReadBCD64(this byte data, byte dig)
        {
            long result = Convert.ToInt64(data.ToString("X"));
            return result * (long)Math.Pow(100, dig - 1);
        }

        public static string ReadStringLittle(this byte[] read, int offset, int len)
        {
            return ReadStringLittle(read, offset, len, Encoding.GetEncoding("GBK"));
        }

        public static string ReadStringLittle(this Span<byte> read, int offset, int len)
        {
            return ReadStringLittle(read, offset, len, Encoding.GetEncoding("GBK"));
        }

        public static string ReadStringLittle(this Span<byte> read, int offset)
        {
            return ReadStringLittle(read, offset, read.Length, Encoding.GetEncoding("GBK"));
        }

        public static string ReadStringLittle(this byte[] read, int offset, int len,Encoding encoding)
        {
            return encoding.GetString(read, offset, len- offset).Trim('\0');
        }

        public static string ReadStringLittle(this Span<byte> read, int offset, int len, Encoding encoding)
        {
            return encoding.GetString(read.ToArray(), offset, len).Trim('\0');
        }

        public static string ReadStringLittle(this Span<byte> read, int offset, Encoding encoding)
        {
            return encoding.GetString(read.ToArray(), offset, read.Length- offset).Trim('\0');
        }

        public static DateTime ReadDateTimeLittle(this Span<byte> read, int offset, int len)
        {
            return new DateTime(
                (read[offset++]).ReadBCD32(1) + DateLimitYear,
                (read[offset++]).ReadBCD32(1),
                (read[offset++]).ReadBCD32(1),
                (read[offset++]).ReadBCD32(1),
                (read[offset++]).ReadBCD32(1),
                (read[offset++]).ReadBCD32(1));
        }

        public static void WriteLittle(this Span<byte> write, int data, int offset, int len)
        {
            int n = 1;
            for (int i = 0; i < len; i++)
            {
                write[offset] = (byte)(data >> 8 * (len - n));
                n++;
                offset++;
            }
        }

        public static void WriteLittle(this Span<byte> write, byte bit, int offset)
        {
            write[offset] = bit;
        }

        public static void WriteLittle(this Span<byte> write, Span<byte> bit, int offset)
        {
            for (var i = 0; i < bit.Length; i++)
            {
                write[offset] = bit[i];
                offset++;
            }
        }

        public static void WriteLittle(this Span<byte> write, string str, int offset, Encoding coding)
        {
            write.WriteLittle(coding.GetBytes(str), offset);
        }

        public static void WriteLittle(this Span<byte> write, string str, int offset)
        {
           WriteLittle(write, str, offset, Encoding.GetEncoding("GBK"));
        }
      
        public static void WriteLittle(this Span<byte> write, DateTime date, int offset)
        {
            write[offset++] = ((byte)(date.Year - DateLimitYear)).ToBcdByte();
            write[offset++] = ((byte)(date.Month)).ToBcdByte();
            write[offset++] = ((byte)(date.Day)).ToBcdByte();
            write[offset++] = ((byte)(date.Hour)).ToBcdByte();
            write[offset++] = ((byte)(date.Minute)).ToBcdByte();
            write[offset++] = ((byte)(date.Second)).ToBcdByte();
        }

        public static void WriteLatLng(this Span<byte> write, double latlng, int offset)
        {
            WriteLittle(write, (int)(Math.Pow(10, 6) * latlng), offset, 4);
        }

        public static void WriteBCDLittle(this Span<byte> write, string data, int offset, int len)
        {
            string bcd = data.PadLeft(len * 2, '0');
            for (int i = 0; i < len; i++)
            {
                write[offset + i] = Convert.ToByte(bcd.Substring(i * 2, 2), 16);
            }
        }

        public static IEnumerable<byte> ToBytes(this string data,Encoding coding)
        {
            return coding.GetBytes(data);
        }

        public static IEnumerable<byte> ToBytes(this string data)
        {
            return ToBytes(data, Encoding.GetEncoding("GBK"));
        }

        public static IEnumerable<byte> ToBytes(this int data, int len)
        {
            List<byte> bytes = new List<byte>();
            int n = 1;
            for (int i = 0; i < len; i++)
            {
                bytes.Add((byte)(data >> 8 * (len - n)));
                n++;
            }
            return bytes;
        }

        /// <summary>
        /// 异或
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte ToXor(this Span<byte> buf, int offset, int len)
        {
            byte result = buf[offset];
            for (int i = offset+1; i < len; i++)
            {
                result = (byte)(result ^ buf[i]);
            }
            return result;
        }

        public static byte ToBcdByte(this byte buf)
        {
            return (byte)Convert.ToInt32(buf.ToString(), 16);
        }

        /// <summary>
        /// 经纬度
        /// </summary>
        /// <param name="latlng"></param>
        /// <returns></returns>
        public static double ToLatLng(this int latlng)
        {
            return Math.Round(latlng / Math.Pow(10, 6), 6);
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator">默认 " "</param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes, string separator = " ")
        {
            return string.Join(separator, bytes.Select(s => s.ToString("X2")));
        }

        /// <summary>
        /// 16进制字符串转16进制数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] ToHexBytes(this string hexString, string separator = " ")
        {
            return hexString.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToByte(s, 16)).ToArray();
        }

        /// <summary>
        /// 16进制字符串转16进制数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToStr2HexBytes(this string hexString)
        {
            //byte[] buf = new byte[hexString.Length / 2];
            //for (int i = 0; i < hexString.Length; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        buf[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16) ;
            //    }

            //}
            byte[] buf = new byte[hexString.Length / 2];
            ReadOnlySpan<char> readOnlySpan = hexString.AsSpan();
            for (int i = 0; i < hexString.Length; i++)
            {
                if (i % 2 == 0)
                {
                    buf[i / 2] = Convert.ToByte(readOnlySpan.Slice(i, 2).ToString(), 16);
                }
            }
            return buf;
            //List<byte> bytes = new List<byte>();
            //while (hexString.Length>0)
            //{
            //    bytes.Add(Convert.ToByte(hexString.AsSpan(0, 2).ToString(), 16));
            //    hexString = hexString.Remove(0,2);
            //}
            //return Regex.Replace(hexString, @"(\w{2})", "$1 ").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToByte(s, 16)).ToArray();
        }

    }
}
