using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using GPS.PubSub.Abstractions;
using JT808.Protocol.Enums;
using GPS.Dispatcher.Abstractions;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808Server : AppServer<JT808Session<JT808RequestInfo>, JT808RequestInfo>
    {
        public IProducerFactory ProducerFactory { get; private set; }

        public IConsumerFactory ConsumerFactory { get; private set; }

        public JT808MsgIdHandler JT808MsgIdHandler { get; private set; }

        public ISourcePackageDispatcher SourcePackageDispatcher { get; private set; }

        public IDeviceMonitoringDispatcher DeviceMonitoringDispatcher { get; private set; }

        private readonly ILogger<JT808Server> log;

        public JT808Server(
            JT808MsgIdHandler jT808MsgIdHandler,
            IConsumerFactory consumerFactory,
            IProducerFactory producerFactory,
            ISourcePackageDispatcher sourcePackageDispatcher,
            IDeviceMonitoringDispatcher deviceMonitoringDispatcher,
            ILoggerFactory loggerFactory)
            : base(new DefaultReceiveFilterFactory<JT808ReceiveFilter, JT808RequestInfo>())
        {
            JT808MsgIdHandler = jT808MsgIdHandler;
            ConsumerFactory = consumerFactory;
            ProducerFactory = producerFactory;
            SourcePackageDispatcher = sourcePackageDispatcher;
            DeviceMonitoringDispatcher = deviceMonitoringDispatcher;
            log = loggerFactory.CreateLogger<JT808Server>();
            log.LogDebug("Init JT808Server");
        }

        public JT808Server(
                 JT808MsgIdHandler jT808MsgIdHandler,
                 IConsumerFactory consumerFactory,
                 IProducerFactory producerFactory,
                 ILoggerFactory loggerFactory)
                    : base(new DefaultReceiveFilterFactory<JT808ReceiveFilter, JT808RequestInfo>())
        {
            JT808MsgIdHandler = jT808MsgIdHandler;
            ConsumerFactory = consumerFactory;
            ProducerFactory = producerFactory;
            SourcePackageDispatcher = null;
            DeviceMonitoringDispatcher = null;
            log = loggerFactory.CreateLogger<JT808Server>();
            log.LogDebug("Init JT808Server");
        }

        public JT808Server(JT808MsgIdHandler jT808MsgIdHandler,
                            ILoggerFactory loggerFactory)
                        : base(new DefaultReceiveFilterFactory<JT808ReceiveFilter, JT808RequestInfo>())
        {
            JT808MsgIdHandler = jT808MsgIdHandler;
            ConsumerFactory = null;
            ProducerFactory = null;
            SourcePackageDispatcher = null;
            DeviceMonitoringDispatcher = null;
            log = loggerFactory.CreateLogger<JT808Server>();
            log.LogDebug("Init JT808Server");
        }

        protected override void OnStarted()
        {
            try
            {
                ConsumerFactory
                            .Subscribe(PubSubConstants.UnificationSend)
                            .OnMessage(msg =>
                            {
                                try
                                {
                                    GetSessions(f => f.TerminalPhoneNo == msg.Key).FirstOrDefault()?.TrySend(msg.data, 0, msg.data.Length);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error("Send Error", ex);
                                }
                            });
            }
            catch (Exception ex)
            {
                Logger.Error("Consumer Error", ex);
            }
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            try
            {
                ConsumerFactory.Unsubscribe();
                ProducerFactory.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Error("Stopped Error", ex);
            }
            base.OnStopped();
        }
    }
}
