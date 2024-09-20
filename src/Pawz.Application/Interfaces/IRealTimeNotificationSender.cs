using Pawz.Domain.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

/// <summary>
/// Defines a contract for sending real-time notifications to a user.
/// </summary>
public interface IRealTimeNotificationSender
{
    /// <summary>
    /// Sends a real-time notification to a specific user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to whom the notification will be sent.</param>
    /// <param name="notification">The notification to be sent.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the asynchronous send operation.</returns>
    Task<Result<bool>> SendNotificationAsync(string userId, string notification, CancellationToken cancellationToken);
}
