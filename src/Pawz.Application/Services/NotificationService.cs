using AutoMapper;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Models.NotificationModels;
using Pawz.Domain.Common;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Application.Services;

/// <summary>
/// Provides functionality for managing notifications, including creating, updating, retrieving,
/// marking as read, and deleting notifications. This service interacts with the repository layer 
/// and uses unit of work to ensure data consistency. Additionally, it handles real-time notification 
/// sending when necessary.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<NotificationService> _logger;
    private readonly IRealTimeNotificationSender _realTimeNotificationSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationService"/> class.
    /// </summary>
    /// <param name="notificationRepository">The repository for managing notifications in the data layer.</param>
    /// <param name="unitOfWork">The unit of work for ensuring atomic transactions in data operations.</param>
    /// <param name="mapper">The mapper for converting between domain and response models.</param>
    /// <param name="logger">The logger for logging informational and error messages.</param>
    /// <param name="realTimeNotificationSender">The real-time notification sender for sending notifications.</param>
    public NotificationService(
    INotificationRepository notificationRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<NotificationService> logger,
    IRealTimeNotificationSender realTimeNotificationSender)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _realTimeNotificationSender = realTimeNotificationSender;
    }

    /// <summary>
    /// Creates or updates a notification for a specified recipient based on the provided notification request.
    /// If an existing notification is found (based on the sender, recipient, and pet), it will be updated.
    /// Otherwise, a new notification will be created.
    /// </summary>
    /// <param name="request">The notification request containing the sender, recipient, message, and other details.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
    /// <returns>A task representing the operation. The task result contains the created or updated notification response.</returns>
    public async Task<Result<NotificationResponse>> CreateNotificationAsync(NotificationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var notification = new Notification
            {
                SenderId = request.SenderId,
                RecipientId = request.RecipientId,
                PetId = request.PetId,
                Type = request.Type
            };

            var existingNotification = await _notificationRepository.GetExistingNotificationAsync(notification, cancellationToken);

            notification = _mapper.Map<Notification>(request);

            if (existingNotification != null)
            {
                existingNotification.Message = request.Message;
                existingNotification.IsRead = false;
                existingNotification.Type = request.Type;
                await _notificationRepository.UpdateAsync(existingNotification, cancellationToken);
            }
            else
            {
                notification.CreatedAt = DateTime.UtcNow;
                notification.IsRead = false;
                await _notificationRepository.InsertAsync(notification, cancellationToken);
            }

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                return Result<NotificationResponse>.Failure("Failed to create/update notification");
            }

            var response = _mapper.Map<NotificationResponse>(existingNotification ?? notification);

            await _realTimeNotificationSender.SendNotificationAsync(request.RecipientId, response.Message, cancellationToken);

            return Result<NotificationResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating notification");
            return Result<NotificationResponse>.Failure("An unexpected error occurred");
        }
    }

    /// <summary>
    /// Retrieves a notification by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the notification to be retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
    /// <returns>A task representing the operation. The task result contains the notification response if found, or a failure result if the notification does not exist.</returns>
    public async Task<Result<NotificationResponse>> GetNotificationByUserIdAsync(string userId, int notificationId, CancellationToken cancellationToken)
    {
        try
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(notificationId, cancellationToken);
            if (notification == null || notification.RecipientId != userId)
            {
                return Result<NotificationResponse>.Failure("Notification not found or does not belong to the user");
            }

            var response = _mapper.Map<NotificationResponse>(notification);
            return Result<NotificationResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving notification");
            return Result<NotificationResponse>.Failure("An unexpected error occurred");
        }
    }

    /// <summary>
    /// Retrieves a list of notifications for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose notifications are being retrieved.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
    /// <returns>A task representing the operation. The task result contains a list of notification responses for the user.</returns>
    public async Task<Result<List<NotificationResponse>>> GetNotificationsForUserAsync(string userId, CancellationToken cancellationToken)
    {
        try
        {
            var notifications = await _notificationRepository.GetNotificationsForUserAsync(userId, cancellationToken);
            var response = _mapper.Map<List<NotificationResponse>>(notifications);
            _logger.LogInformation("Mapped notifications: {@Notifications}", response);

            return Result<List<NotificationResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving notifications for user");
            return Result<List<NotificationResponse>>.Failure("An unexpected error occurred");
        }
    }

    /// <summary>
    /// Marks a specific notification as read based on its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the notification to be marked as read.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating whether the operation was successful or not.</returns>
    public async Task<Result<bool>> MarkNotificationAsReadAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Attempting to mark notification as read. ID: {id}");

            var notification = await _notificationRepository.GetNotificationByIdAsync(id, cancellationToken);
            if (notification == null)
            {
                _logger.LogWarning($"Notification not found. ID: {id}");

                return Result<bool>.Failure("Notification not found");
            }

            notification.IsRead = true;
            await _notificationRepository.UpdateAsync(notification, cancellationToken);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                _logger.LogWarning($"Failed to mark notification as read. ID: {id}");

                return Result<bool>.Failure("Failed to mark notification as read");
            }
            _logger.LogInformation($"Successfully marked notification as read. ID: {id}");

            var response = _mapper.Map<NotificationResponse>(notification);
            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking notification as read");
            return Result<bool>.Failure("An unexpected error occurred");
        }
    }

    /// <summary>
    /// Deletes a specific notification based on its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the notification to be deleted.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to cancel the operation.</param>
    /// <returns>A task representing the operation. The task result contains a boolean indicating whether the deletion was successful or not.</returns>
    public async Task<Result<bool>> DeleteNotificationAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(id, cancellationToken);
            if (notification == null)
            {
                return Result<bool>.Failure("Notification not found");
            }

            await _notificationRepository.DeleteAsync(notification, cancellationToken);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                return Result<bool>.Failure("Failed to delete notification");
            }

            return Result<bool>.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting notification");
            return Result<bool>.Failure("An unexpected error occurred");
        }
    }
}
