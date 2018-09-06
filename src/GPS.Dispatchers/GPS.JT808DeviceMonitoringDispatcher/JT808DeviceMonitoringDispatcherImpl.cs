using GPS.Dispatcher.Abstractions;
using GPS.PubSub.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS.JT808DeviceMonitoringDispatcher
{
    public class JT808DeviceMonitoringDispatcherImpl : IDeviceMonitoringDispatcher
    {
        private readonly IProducerFactory _producerFactory;

        public JT808DeviceMonitoringDispatcherImpl(IProducerFactory producerFactory)
        {
            _producerFactory = producerFactory;
        }

        public Task SendAsync(string deviceKey, byte[] data)
        {
            _producerFactory.CreateProducer(DispatcherConstants.DeviceMonitoringTopic).ProduceAsync(deviceKey, data);
            return Task.CompletedTask;
        }
    }
}
