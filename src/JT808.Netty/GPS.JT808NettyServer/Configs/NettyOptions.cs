using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808NettyServer.Configs
{
    public class NettyOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public NettyIdleStateOptions IdleStateOptions { get; set; }
        public List<string> IpWhiteList { get; set; } = new List<string>();
        public bool IpWhiteListDisabled { get; set; }
    }
}
