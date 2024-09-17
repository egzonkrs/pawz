using Pawz.Application.Interfaces;
using Pawz.Application.Models.NotificationModels;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Services;

public class SignalRNotificationSender : IRealTimeNotificationSender
{
    private readonly INotificationHubContext _hubContext;
    public SignalRNotificationSender(INotificationHubContext hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task SendNotificationAsync(string userId, NotificationResponse notification, CancellationToken cancellationToken)
    {
        await _hubContext.SendToUserAsync(userId, "ReceiveNotification", notification, cancellationToken);
    }
}
