using Microsoft.AspNetCore.SignalR;

namespace HFP.Api.Hubs
{
    public class SystemHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public async Task SendWelcomeMessage(string title, string message)
        {
            await Clients.All.SendAsync("CardInserted", new
            {
                title,
                message
            });
        }
    }
}
