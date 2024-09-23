using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pawz.Application.Interfaces;
using Pawz.Application.Models.NotificationModels;
using Pawz.Web.Models.NotificationModels;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;
    private readonly IUserAccessor _userAccessor;
    private readonly IMapper _mapper;
    private readonly ILogger<NotificationController> _logger;

    public NotificationController(INotificationService notificationService,
                                  IUserAccessor userAccessor,
                                  ILogger<NotificationController> logger,
                                  IMapper mapper)
    {
        _notificationService = notificationService;
        _userAccessor = userAccessor;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequestViewModel model)
    {
        var request = _mapper.Map<NotificationRequest>(model);
        request.SenderId = _userAccessor.GetUserId();

        var result = await _notificationService.CreateNotificationAsync(request, HttpContext.RequestAborted);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserNotifications()
    {
        var userId = _userAccessor.GetUserId();
        var result = await _notificationService.GetNotificationsForUserAsync(userId, HttpContext.RequestAborted);

        if (result.IsSuccess)
        {
            _logger.LogInformation("Notifications fetched: {@Notifications}", result.Value);
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost("markAsRead/{id}")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        _logger.LogInformation($"Received request to mark notification as read. ID: {id}");

        if (id <= 0)
        {
            return BadRequest("Invalid notification ID");
        }
        var result = await _notificationService.MarkNotificationAsReadAsync(id, HttpContext.RequestAborted);

        if (result.IsSuccess)
        {
            _logger.LogInformation($"Successfully marked notification as read. ID: {id}");

            return Ok(new { success = true, message = "Notification marked as read" });

        }
        _logger.LogWarning($"Failed to mark notification as read. ID: {id}. Error: {result.Errors}");

        return BadRequest(new { success = false, message = result.Errors });
    }

}
