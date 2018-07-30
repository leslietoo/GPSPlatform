using JT808.MsgIdExtensions;
using JT808.Protocol;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JT808.Protocol.Extensions;
using System.Threading;
using System.Linq;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808Server : AppServer<JT808Session<JT808RequestInfo>, JT808RequestInfo>
    {
        //private JT808_UnificationSend_Consumer jT808_UnificationSend_Consumer;

        //private CancellationTokenSource jT808_UnificationSend_Consumer_CancellationTokenSource;

        public JT808Server() : base(new DefaultReceiveFilterFactory<JT808ReceiveFilter, JT808RequestInfo>())
        {
            //JT808GlobalConfigs.RegisterMessagePackFormatter();
        }

        protected override void OnStarted()
        {
            //try
            //{
            //    jT808_UnificationSend_Consumer_CancellationTokenSource = new CancellationTokenSource();
            //    jT808_UnificationSend_Consumer = new JT808_UnificationSend_Consumer(new Dictionary<string, object>
            //    {
            //          { "group.id", "GatewayUnificationSend" },
            //          { "enable.auto.commit", true },
            //    });
            //    jT808_UnificationSend_Consumer.MsgIdConsumer.OnMessage += (_, msg) =>
            //    {
            //        // todo: 处理下发数据
            //        Logger.Debug($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value.ToHexString()}");
            //        try
            //        {
            //            GetSessions(f=>f.TerminalPhoneNo== msg.Key).FirstOrDefault()?.TrySend(msg.Value, 0, msg.Value.Length);
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.Error("UnificationSend Error OnMessage", ex);
            //        }
            //    };
            //    jT808_UnificationSend_Consumer.MsgIdConsumer.OnError += (_, error) =>
            //    {
            //        Logger.Debug($"Error: {error}");
            //    };
            //    jT808_UnificationSend_Consumer.MsgIdConsumer.OnConsumeError += (_, msg) =>
            //    {
            //        Logger.Debug($"Error consuming from topic/partition/offset {msg.Topic}/{msg.Partition}/{msg.Offset}: {msg.Error}");
            //    };
            //    jT808_UnificationSend_Consumer.MsgIdConsumer.Subscribe(jT808_UnificationSend_Consumer.JT808MsgIdTopic);
            //    Task.Run(() =>
            //    {
            //        while (!jT808_UnificationSend_Consumer_CancellationTokenSource.IsCancellationRequested)
            //        {
            //            jT808_UnificationSend_Consumer.MsgIdConsumer.Poll(TimeSpan.FromMilliseconds(100));
            //        }
            //    }, jT808_UnificationSend_Consumer_CancellationTokenSource.Token);
            //}
            //catch (AggregateException ex)
            //{
            //    Logger.Error("OnStarted AggregateError", ex);
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error("OnStarted Error", ex);
            //}
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            //try
            //{
            //    jT808_UnificationSend_Consumer_CancellationTokenSource.Cancel();
            //    jT808_UnificationSend_Consumer.MsgIdConsumer.Unsubscribe();
            //    jT808_UnificationSend_Consumer.MsgIdConsumer.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error("OnStopped Error", ex);
            //}
            base.OnStopped();
        }
    }
}
