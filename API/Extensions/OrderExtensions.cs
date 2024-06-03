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

        public static IQueryable<Order> Sort(this IQueryable<Order> querry, string? orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return querry;

            querry = orderBy switch
            {
                "price" => querry.OrderBy(p => p.Total),
                "priceDesc" => querry.OrderByDescending(p => p.Total),
                "date" => querry.OrderBy(p => p.OrderDate),
                "dateDesc" => querry.OrderByDescending(p => p.OrderDate),
                _ => querry.OrderBy(p => p.User),
            };

            return querry;
        }

    }
}
