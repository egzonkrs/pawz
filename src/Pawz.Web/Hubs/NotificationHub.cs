using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Domain.Common;
using System;
using System.Threading.Tasks;

namespace Pawz.Web.Hubs;

/// <summary>
/// A SignalR hub for handling real-time notifications.
/// </summary>
public class NotificationHub : Hub
{
    private readonly IUserAccessor _userAccessor;
    private readonly ILogger<NotificationHub> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationHub"/> class.
    /// </summary>
    /// <param name="userAccessor">The service used to access user-related information.</param>
    public NotificationHub(IUserAccessor userAccessor, ILogger<NotificationHub> logger)
    {
        _userAccessor = userAccessor;
        _logger = logger;
    }

    /// <summary>
    /// Sends a real-time notification to a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user who will receive the notification.</param>
    /// <param name="message">The message to be sent in the notification.</param>
    /// <returns>A task that represents the asynchronous operation of sending the notification.</returns>
    public async Task SendNotification(string userId, string message)
    {
        try
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", new { Message = message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending notification to user {userId}: {ex.Message}");
        }
    }

    /// <summary>
    /// Handles the event when a client connects to the SignalR hub.
    /// Adds the client to a SignalR group based on the user's first name.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation of handling the connection.</returns>
    public override async Task<Result<bool>> OnConnectedAsync()
    {
        try
        {
            var currentUser = _userAccessor.GetUserId();

            if (string.IsNullOrEmpty(currentUser))
            {
                _logger.LogError("User connection failed: invalid user ID.");
                return Result<bool>.Failure(NotificationErrors.InvalidRecipient);
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, currentUser);
            _logger.LogInformation("User {UserId} successfully connected with ConnectionId {ConnectionId}", currentUser, Context.ConnectionId);

            await base.OnConnectedAsync();
            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while attempting to connect user.");
            return Result<bool>.Failure(NotificationErrors.UserConnectionFailed);
        }
    }

    /// <summary>
    /// Handles the event when a client disconnects from the SignalR hub.
    /// Removes the client from the SignalR group based on the user's first name.
    /// </summary>
    /// <param name="exception">An optional exception parameter in case the disconnection was caused by an error.</param>
    /// <returns>A task that represents the asynchronous operation of handling the disconnection.</returns>
    public override async Task<Result<bool>> OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            var currentUser = _userAccessor.GetUserId();

            if (string.IsNullOrEmpty(currentUser))
            {
                _logger.LogError("User disconnection failed: invalid user ID.");
                return Result<bool>.Failure(NotificationErrors.InvalidRecipient);
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, currentUser);
            _logger.LogInformation("User {UserId} successfully disconnected and removed from group with ConnectionId {ConnectionId}", currentUser, Context.ConnectionId);

            if (exception != null)
            {
                _logger.LogError(exception, $"Disconnection error: {exception.Message}");
                return Result<bool>.Failure(NotificationErrors.UserDisconnectionFailed);
            }

            await base.OnDisconnectedAsync(exception);
            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while attempting to disconnect user.");
            return Result<bool>.Failure(NotificationErrors.UnexpectedError);
        }
    }
}
