using Microsoft.AspNetCore.SignalR;
using Pawz.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Hubs;

public class SignalRHubContext : INotificationHubContext
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public SignalRHubContext(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendToUserAsync<T>(string userId, string method, T arg, CancellationToken cancellationToken)
    {
        await _hubContext.Clients.User(userId).SendAsync(method, arg, cancellationToken);
    }
}
