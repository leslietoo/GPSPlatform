using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using JT808.Protocol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JT808.MsgId0x0200Services.Hubs
{
    //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public class AlarmHub : Hub
    {
        [Authorize(JwtBearerDefaults.AuthenticationScheme)]
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
