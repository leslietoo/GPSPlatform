using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.JT808SampleDeviceMonitoring.Configs
{
    public  class LogMonitioringOptions
    {
        /// <summary>
        /// 以,逗号隔开
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 默认一小时
        /// </summary>
        public int MonitoringTime { get; set; } = 1;
        /// <summary>
        /// 监控状态
        /// </summary>
        public bool MonitoringStatus { get; set; }
    }
}
