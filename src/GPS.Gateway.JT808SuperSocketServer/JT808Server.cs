using JT808.MsgIdExtensions;
using JT808.Protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Threading.Tasks;
using JT808.Protocol.Extensions;
using System.Threading;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808Server : AppServer<JT808Session<JT808RequestInfo>, JT808RequestInfo>
    {
        private readonly JT808_UnificationSend_Consumer JT808_UnificationSend_Consumer;

        public JT808_0x0200_Producer JT808_0X0200_Producer { get; private set; }

        private CancellationTokenSource jT808_UnificationSend_Consumer_CancellationTokenSource;

        public JT808MsgIdHandler JT808MsgIdHandler { get; private set; }

        private readonly ILogger<JT808Server> log;

        public JT808Server(
            JT808MsgIdHandler jT808MsgIdHandler,
            JT808_UnificationSend_Consumer jT808_UnificationSend_Consumer,
            JT808_0x0200_Producer jT808_0X0200_Producer,
            ILoggerFactory loggerFactory) : base(new DefaultReceiveFilterFactory<JT808ReceiveFilter, JT808RequestInfo>())
        {
            
            JT808MsgIdHandler = jT808MsgIdHandler;
            jT808_UnificationSend_Consumer_CancellationTokenSource = new CancellationTokenSource();
            JT808_UnificationSend_Consumer = jT808_UnificationSend_Consumer;
            JT808_0X0200_Producer = jT808_0X0200_Producer;
            log = loggerFactory.CreateLogger<JT808Server>();
            JT808GlobalConfigs.RegisterMessagePackFormatter();
            log.LogDebug("Init JT808Server");
        }

        protected override void OnStarted()
        {
            if (JT808_UnificationSend_Consumer != null)
            {
                try
                {
                    JT808_UnificationSend_Consumer.MsgIdConsumer.OnMessage += (_, msg) =>
                    {
                        // todo: 处理下发数据
                        Logger.Debug($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value.ToHexString()}");
                        try
                        {
                            GetSessions(f => f.TerminalPhoneNo == msg.Key).FirstOrDefault()?.TrySend(msg.Value, 0, msg.Value.Length);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("UnificationSend Error OnMessage", ex);
                        }
                    };
                    JT808_UnificationSend_Consumer.MsgIdConsumer.OnError += (_, error) =>
                    {
                        Logger.Debug($"Error: {error}");
                    };
                    JT808_UnificationSend_Consumer.MsgIdConsumer.OnConsumeError += (_, msg) =>
                    {
                        Logger.Debug($"Error consuming from topic/partition/offset {msg.Topic}/{msg.Partition}/{msg.Offset}: {msg.Error}");
                    };
                    JT808_UnificationSend_Consumer.MsgIdConsumer.Subscribe(JT808_UnificationSend_Consumer.JT808MsgIdTopic);
                    Task.Run(() =>
                    {
                        while (!jT808_UnificationSend_Consumer_CancellationTokenSource.IsCancellationRequested)
                        {
                            JT808_UnificationSend_Consumer.MsgIdConsumer.Poll(TimeSpan.FromMilliseconds(100));
                        }
                    }, jT808_UnificationSend_Consumer_CancellationTokenSource.Token);
                }
                catch (AggregateException ex)
                {
                    Logger.Error("OnStarted AggregateError", ex);
                }
                catch (Exception ex)
                {
                    Logger.Error("OnStarted Error", ex);
                }
            }
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            try
            {
                jT808_UnificationSend_Consumer_CancellationTokenSource.Cancel();
                if (JT808_UnificationSend_Consumer != null)
                {
                    JT808_UnificationSend_Consumer.MsgIdConsumer.Unsubscribe();
                    JT808_UnificationSend_Consumer.MsgIdConsumer.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("OnStopped Error", ex);
            }
            base.OnStopped();
        }
    }
}
