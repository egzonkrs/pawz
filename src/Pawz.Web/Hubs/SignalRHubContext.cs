using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Domain.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Web.Hubs;

/// <summary>
/// Implements the INotificationHubContext to handle real-time notification communication via SignalR.
/// </summary>
public class SignalRHubContext : INotificationHubContext
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly ILogger<SignalRHubContext> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SignalRHubContext"/> class.
    /// </summary>
    /// <param name="hubContext">The SignalR hub context used for sending notifications.</param>
    /// <param name="logger">The logger used for logging any errors or important information.</param>
    public SignalRHubContext(IHubContext<NotificationHub> hubContext, ILogger<SignalRHubContext> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    /// <summary>
    /// Sends a real-time message to a specific user via SignalR.
    /// </summary>
    /// <typeparam name="T">The type of the message being sent.</typeparam>
    /// <param name="userId">The ID of the user who will receive the message.</param>
    /// <param name="method">The method that will be invoked on the client-side.</param>
    /// <param name="arg">The argument to pass to the method on the client-side.</param>
    /// <param name="cancellationToken">The cancellation token for handling operation cancellation.</param>
    /// <returns>A task representing the asynchronous operation of sending the message.</returns>
    public async Task<Result<bool>> SendToUserAsync<T>(string userId, string method, T arg, CancellationToken cancellationToken)
    {
        try
        {
            await _hubContext.Clients.User(userId).SendAsync(method, arg, cancellationToken);

            _logger.LogInformation("Successfully sent message to user {UserId} using method {Method}", userId, method);
            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed sending message via SignalR to user {UserId} using method {Method}", userId, method);
            return Result<bool>.Failure(NotificationErrors.SendingFailed(userId));
        }
    }
}
