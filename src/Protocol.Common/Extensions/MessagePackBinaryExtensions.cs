using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Protocol.Common.Extensions
{
    public static class MessagePackBinaryExtensions
    {
#if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
        public static int WriteUInt16(ref byte[] bytes, int offset, ushort value)
        {
            if (value <= MessagePackRange.MaxFixPositiveInt)
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
                bytes[offset] = unchecked((byte)value);
                return 1;
            }
            else if (value <= byte.MaxValue)
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
                bytes[offset] = unchecked((byte)value);
                return 2;
            }
            else
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
                bytes[offset] = unchecked((byte)(value >> 8));
                bytes[offset + 1] = unchecked((byte)value);
                return 2;
            }
        }
#if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
        public static int WriteUInt32(ref byte[] bytes, int offset, uint value)
        {
            if (value <= MessagePackRange.MaxFixPositiveInt)
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
                bytes[offset] = unchecked((byte)value);
                return 1;
            }
            else if (value <= byte.MaxValue)
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
                bytes[offset] = unchecked((byte)value);
                return 1;
            }
            else if (value <= ushort.MaxValue)
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
                bytes[offset] = unchecked((byte)(value >> 8));
                bytes[offset + 1] = unchecked((byte)value);
                return 2;
            }
            else
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 4);
                bytes[offset] = unchecked((byte)(value >> 24));
                bytes[offset + 1] = unchecked((byte)(value >> 16));
                bytes[offset + 2] = unchecked((byte)(value >> 8));
                bytes[offset + 3] = unchecked((byte)value);
                return 4;
            }
        }
#if NETSTANDARD
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
        public static int WriteInt32(ref byte[] bytes, int offset, int value)
        {
            if (value <= MessagePackRange.MaxFixPositiveInt)
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
                bytes[offset] = unchecked((byte)value);
                return 1;
            }
            else if (value <= byte.MaxValue)
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 1);
                bytes[offset] = unchecked((byte)value);
                return 1;
            }
            else if (value <= ushort.MaxValue)
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 2);
                bytes[offset] = unchecked((byte)(value >> 8));
                bytes[offset + 1] = unchecked((byte)value);
                return 2;
            }
            else
            {
                //MessagePackBinary.EnsureCapacity(ref bytes, offset, 4);
                bytes[offset] = unchecked((byte)(value >> 24));
                bytes[offset + 1] = unchecked((byte)(value >> 16));
                bytes[offset + 2] = unchecked((byte)(value >> 8));
                bytes[offset + 3] = unchecked((byte)value);
                return 4;
            }
        }

#if NETSTANDARD
      //  [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
        //public static int WriteDateTime(ref byte[] bytes, int offset, DateTime dateTime)
        //{
            
        //}

    }
}
