using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Services;

/// <summary>
/// Sends real-time notifications to users using SignalR.
/// Implements the <see cref="IRealTimeNotificationSender"/> interface.
/// </summary>
public class SignalRNotificationSender : IRealTimeNotificationSender
{
    private readonly INotificationHubContext _hubContext;
    private readonly ILogger<SignalRNotificationSender> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SignalRNotificationSender"/> class.
    /// </summary>
    /// <param name="hubContext">The hub context responsible for sending notifications to users.</param>
    /// <param name="logger">The logger used to log errors and important information.</param>
    public SignalRNotificationSender(INotificationHubContext hubContext, ILogger<SignalRNotificationSender> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    /// <summary>
    /// Sends a real-time notification to a specified user using SignalR.
    /// </summary>
    /// <param name="userId">The ID of the user who will receive the notification.</param>
    /// <param name="notification">The notification message to be sent to the user.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation of sending the notification.</returns>
    public async Task SendNotificationAsync(string userId, string notification, CancellationToken cancellationToken)
    {
        try
        {
            await _hubContext.SendToUserAsync(userId, "ReceiveNotification", notification, cancellationToken);
            _logger.LogInformation($"Successfully sent notification to user {userId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending notification to user {userId}: {ex.Message}");
            throw;
        }
    }
}
