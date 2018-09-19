using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808NettyServer.Configs
{
    public class NettyIdleStateOptions
    {
        public int ReaderIdleTimeSeconds { get; set; }

        public int WriterIdleTimeSeconds { get; set; }

        public int AllIdleTimeSeconds { get; set; }
    }
}
