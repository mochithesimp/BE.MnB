using API.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace API.Extensions
{
    public static class OrderExtensions
    {
        public static IQueryable<Order> FilterStatus(this IQueryable<Order> querry, int status)
        {
            if (status == 1)
            {
                querry = querry.Where(o => o.OrderStatus == "Pending");
            }

            if (status == 2)
            {
                querry = querry.Where(o => o.OrderStatus == "Submitted");
            }

            if (status == 3)
            {
                querry = querry.Where(o => o.OrderStatus == "Completed");
            }

            if (status == 4)
            {
                querry = querry.Where(o => o.OrderStatus == "Canceled");
            }

            return querry;
        }
    }
}
