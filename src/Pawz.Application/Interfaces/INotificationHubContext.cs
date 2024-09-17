using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

/// <summary>
/// Defines a contract for sending notifications to users via a hub context.
/// </summary>
public interface INotificationHubContext
{
    /// <summary>
    /// Sends a notification to a specific user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to whom the notification will be sent.</param>
    /// <param name="method">The method name to be invoked on the client side.</param>
    /// <param name="arg">The argument to pass to the client-side method.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete, allowing the operation to be cancelled.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    Task SendToUserAsync(string userId, string method, object arg, CancellationToken cancellationToken);

}
