using JT808.MsgIdExtensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using JT808.Protocol;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace JT808.MsgId0x0200WebSocket.Hubs
{
    public class AlarmHub : Hub
    {
        private readonly JT808_0x0200_Consumer jT808_0X0200_Consumer;

        private readonly CancellationTokenSource cts;

        public AlarmHub()
        {
            jT808_0X0200_Consumer = new JT808_0x0200_Consumer(new Dictionary<string, object>
                        {
                            { "group.id", "JT808_0x0200_WebSocket_Alarm" },
                            { "enable.auto.commit", true },
                            { "bootstrap.servers", "172.16.19.120:9092" }
                        });
            cts = new CancellationTokenSource();
            jT808_0X0200_Consumer.MsgIdConsumer.OnMessage += (_, msg) =>
            {
                // todo: 处理定位数据
                Clients.All.SendAsync(JsonConvert.SerializeObject(JT808Serializer.Deserialize<JT808Package>(msg.Value)));
            };
            jT808_0X0200_Consumer.MsgIdConsumer.OnError += (_, error) =>
            {
                
            };
            jT808_0X0200_Consumer.MsgIdConsumer.OnConsumeError += (_, msg) =>
            {
                
            };
            Task.Run(() =>
            {
                while (!cts.IsCancellationRequested)
                {
                    jT808_0X0200_Consumer.MsgIdConsumer.Poll(TimeSpan.FromMilliseconds(100));
                }
            }, cts.Token);
        }

        public void Send(string name, string message)
        {
            Clients.All.SendAsync(name, message);
        }
    }
}
