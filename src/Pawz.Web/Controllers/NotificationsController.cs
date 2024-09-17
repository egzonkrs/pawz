using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pawz.Application.Interfaces;
using Pawz.Application.Models.NotificationModels;
using Pawz.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pawz.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService,
                                      IUserAccessor userAccessor,
                                      IMapper mapper)
        {
            _notificationService = notificationService;
            _userAccessor = userAccessor;
            _mapper = mapper;
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
                var viewModels = _mapper.Map<List<NotificationRequestViewModel>>(result.Value);
                return Ok(viewModels);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("markAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var result = await _notificationService.MarkNotificationAsReadAsync(id, HttpContext.RequestAborted);

            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
