using GiveAwayInsider_Business.Repository.IRepository;
using GiveAwayInsider_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiveAwayInsider_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "ApiKey, Bearer")]
    public class NotificationController : Controller
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpGet("{userId}")]
        [ActionName("get")]
        public async Task<IActionResult> Get(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var notifications = await _notificationRepository.GetNotifications(userId);

            return Ok(notifications);
        }

        [HttpGet]
        [ActionName("all")]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationRepository.GetAll();

            return Ok(notifications);
        }

        [HttpPost]
        [ActionName("create")]
        public async Task<IActionResult> Create([FromBody] NotificationDTO notificationDTO)
        {
            var created = await _notificationRepository.CreateNotification(notificationDTO);
            return Ok(created);
        }

        [HttpPost]
        [ActionName("update")]
        public async Task<IActionResult> Update([FromBody] NotificationDTO notificationDTO)
        {
            var updated = await _notificationRepository.UpdateNotification(notificationDTO);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ActionName("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var result = await _notificationRepository.DeleteNotification(id.Value);
            if (result == 1)
                return StatusCode(202);
            else
                return NotFound();
        }
    }
}
