using GPS.Dispatcher.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS.JT808DeviceMonitoringDispatcher
{
    public class JT808DeviceMonitoringDispatcherImpl : IDeviceMonitoringDispatcher
    {
        public async Task SendAsync(string deviceKey, byte[] data)
        {
            await Task.CompletedTask;
        }
    }
}
