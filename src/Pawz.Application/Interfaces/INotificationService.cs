using Pawz.Application.Models.NotificationModels;
using Pawz.Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Interfaces;

public interface INotificationService
{
    /// <summary>
    /// Creates a new notification.
    /// </summary>
    /// <param name="notificationRequest">The notification request to create.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains the created notification response.</returns>
    Task<Result<NotificationResponse>> CreateNotificationAsync(NotificationRequest notificationRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all notifications for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user whose notifications are to be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a collection of notification responses.</returns>
    Task<Result<NotificationResponse>> GetNotificationByIdAsync(int userId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a list of notifications for a specified user.
    /// </summary>
    /// <param name="userId">The ID of the user whose notifications are being retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains an enumerable list of notification responses.</returns>
    Task<Result<IEnumerable<NotificationResponse>>> GetNotificationsForUserAsync(string userId, CancellationToken cancellationToken);

    /// <summary>
    /// Marks a notification as read.
    /// </summary>
    /// <param name="notificationId">The ID of the notification to mark as read.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> MarkNotificationAsReadAsync(int notificationId, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a notification.
    /// </summary>
    /// <param name="notificationId">The ID of the notification to delete.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating success or failure.</returns>
    Task<Result<bool>> DeleteNotificationAsync(int notificationId, CancellationToken cancellationToken);
}

