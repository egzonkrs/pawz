using Pawz.Application.Interfaces;
using Pawz.Application.Models.NotificationModels;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

public class RealTimeNotificationSender : IRealTimeNotificationSender
{
    private readonly INotificationHubContext _notificationHubContext;

    public RealTimeNotificationSender(INotificationHubContext notificationHubContext)
    {
        _notificationHubContext = notificationHubContext;
    }

    public async Task SendNotificationAsync(string userId, NotificationResponse notification, CancellationToken cancellationToken)
    {
        await _notificationHubContext.SendToUserAsync(userId, "ReceiveNotification", notification, cancellationToken);
    }
}
