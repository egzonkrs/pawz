using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Pawz.Web.Hubs;

public class NotificationHub : Hub
{
    public async Task SendNotification(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", new { Message = message });
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
        await base.OnDisconnectedAsync(exception);
    }
}
