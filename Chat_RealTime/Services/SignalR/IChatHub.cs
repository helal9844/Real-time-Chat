using Microsoft.AspNetCore.SignalR;

namespace Chat_RealTime;

public interface IChatHub
{
    Task ReciveMessage(string user, string message);
}
