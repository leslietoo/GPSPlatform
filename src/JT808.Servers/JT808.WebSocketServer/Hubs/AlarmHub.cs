using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace JT808.WebSocketServer.Hubs
{
    public class AlarmHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
