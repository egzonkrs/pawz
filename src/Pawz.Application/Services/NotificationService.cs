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

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<NotificationService> _logger;
    private readonly IRealTimeNotificationSender _realTimeNotificationSender;

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

    public async Task<Result<NotificationResponse>> CreateNotificationAsync(NotificationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var existingNotification = await _notificationRepository.GetExistingNotificationAsync(
                request.SenderId,
                request.RecipientId,
                request.PetId,
                cancellationToken
            );

            var notification = _mapper.Map<Notification>(request);

            if (existingNotification != null)
            {
                existingNotification.Message = request.Message;
                existingNotification.IsRead = false;
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

            await _realTimeNotificationSender.SendNotificationAsync(request.RecipientId, response, cancellationToken);

            return Result<NotificationResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating/updating notification");
            return Result<NotificationResponse>.Failure("An unexpected error occurred");
        }
    }

    public async Task<Result<NotificationResponse>> GetNotificationByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(id, cancellationToken);
            if (notification == null)
            {
                return Result<NotificationResponse>.Failure("Notification not found");
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
