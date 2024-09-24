using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos;

/// <summary>
/// A repository for managing notifications in the database, inheriting from the generic repository.
/// Provides methods to retrieve, create, and update notifications.
/// </summary>
public class NotificationRepository : GenericRepository<Notification, int>, INotificationRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationRepository"/> class.
    /// </summary>
    /// <param name="context">The application's database context.</param>
    public NotificationRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Retrieves a list of notifications for a specified user, ordered by creation date in descending order.
    /// Includes related Pet and Sender entities.
    /// </summary>
    /// <param name="userId">The unique identifier of the recipient user whose notifications are being retrieved.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains an enumerable list of notifications for the specified user.</returns>
    public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(string userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(n => n.RecipientId == userId)
            .Include(n => n.Pet)
            .Include(n => n.Sender)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a notification by its unique identifier.
    /// Includes related Pet and Sender entities.
    /// </summary>
    /// <param name="id">The unique identifier of the notification.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the notification if found, otherwise null.</returns>
    public async Task<Notification?> GetNotificationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(n => n.Pet)
            .Include(n => n.Sender)
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves an existing notification based on the details in the provided notification object.
    /// </summary>
    /// <param name="notification">The notification object used to search for an existing notification.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the existing notification if found, or null if no matching notification is found.</returns>
    public async Task<Notification?> GetExistingNotificationAsync(Notification request, CancellationToken cancellationToken)
    {
        return await _dbSet
            .FirstOrDefaultAsync(n =>
                n.SenderId == request.SenderId &&
                n.RecipientId == request.RecipientId &&
                n.PetId == request.PetId &&
                n.Type == request.Type,
                cancellationToken);
    }
}
