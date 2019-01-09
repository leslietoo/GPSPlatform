using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.Abstractions
{
    /// <summary>
    /// JT808消息Id处理程序
    /// </summary>
    public interface IJT808MsgIdHandler
    {
        void Processor(byte[] data);
    }
}
