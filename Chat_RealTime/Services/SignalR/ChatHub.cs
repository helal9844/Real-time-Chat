using Microsoft.AspNetCore.SignalR;

namespace Chat_RealTime;

public class ChatHub : Hub<IChatHub>
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.ReciveMessage(user,message);
    }
}
