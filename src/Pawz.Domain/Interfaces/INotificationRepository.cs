using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

/// <summary>
/// Defines a repository interface for managing Notification entities.
/// </summary>
public interface INotificationRepository : IGenericRepository<Notification, int>
{
    /// <summary>
    /// Retrieves all notifications for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user whose notifications are to be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of notifications associated with the specified user.</returns>
    Task<IEnumerable<Notification>> GetNotificationsForUserAsync(string userId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a single Notification entity by its unique ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Notification to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the Notification entity,
    /// or null if no notification with the specified ID is found.
    /// </returns>
    Task<Notification> GetNotificationByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an existing notification based on sender, recipient, and pet ID, if applicable.
    /// </summary>
    /// <param name="senderId">The ID of the sender of the notification.</param>
    /// <param name="recipientId">The ID of the recipient of the notification.</param>
    /// <param name="petId">The optional pet ID associated with the notification.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
    /// <returns>The notification if it exists, or null if no matching notification is found.</returns>
    Task<Notification> GetExistingNotificationAsync(Notification notification, CancellationToken cancellationToken);
}
