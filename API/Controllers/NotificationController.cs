using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly StoreContext _context;

        public NotificationController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("getAllNotisByUser")]
        public async Task<ActionResult<List<NotificationDTO>>> GetNotisByUser(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();

            if (notifications.Count > 0)
            {
                var notisDTO = notifications.Select(toNotificationDTO).ToList();
                return Ok(notisDTO);
            }

            return NotFound();
        }

        [HttpDelete("deleteOneNotification")]
        public async Task<IActionResult> DeleteNotificationById(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);

            if (notification == null)
            {
                return NotFound("Notification not found");
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok("Notification deleted successfully");
        }

        [HttpDelete("deleteAllNotificationsByUser")]
        public async Task<IActionResult> DeleteNotificationsByUser(int userId)
        {
            var notifications = await _context.Notifications.Where(n => n.UserId == userId).ToListAsync();

            if (notifications.Count == 0)
            {
                return NotFound("No notifications found for the specified user");
            }

            _context.Notifications.RemoveRange(notifications);
            await _context.SaveChangesAsync();

            return Ok("All Notifications deleted successfully");
        }

        [HttpPut("updateNotificationReadStatus")]
        public async Task<IActionResult> UpdateNotificationReadStatus(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);

            if (notification == null)
            {
                return NotFound("Notification not found");
            }

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return Ok("Notification read status updated successfully");
        }





        public static NotificationDTO toNotificationDTO(Notification? notification)
        {
            NotificationDTO notiDTO = new NotificationDTO();

            notiDTO.NotificationId = notification.NotificationId;
            notiDTO.UserId = notification.UserId;
            notiDTO.Header = notification.Header;
            notiDTO.Content = notification.Content;
            notiDTO.IsRead = notification.IsRead;
            notiDTO.IsRemoved = notification.IsRemoved;
            notiDTO.CreatedDate = notification.CreatedDate;

            return notiDTO;
        }

    }
}
