using Microsoft.AspNetCore.SignalR;

namespace HFP.Api.Hubs
{
    public class SystemHub : Hub
    {
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
