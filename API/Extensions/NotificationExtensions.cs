using API.Entities;

namespace API.Extensions
{
    public static class NotificationExtensions
    {
        public static Notification createNotification(int userId, string header, string content)
        {
            var notification = new Notification()
            {
                UserId = userId,
                Header = header,
                Content = content,
                IsRead = false,
                IsRemoved = false,
                CreatedDate = DateTime.Now,
            };

            return notification;
        }
    }
}
