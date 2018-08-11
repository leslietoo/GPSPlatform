using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Dispatcher.Abstractions
{
    /// <summary>
    /// 设备监控(点名）分发器
    /// </summary>
    public interface IDeviceMonitoringDispatcher
    {
        Task SendAsync(string deviceKey, byte[] data);
    }
}
