using API.Entities;

namespace API.Extensions
{
    public static class ReviewExtensions
    {
        public static IQueryable<Review> FilterBy(this IQueryable<Review> querry, string? orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return querry;

            querry = orderBy switch
            {
                "rate" => querry.OrderBy(p => p.Rating),
                "rateDesc" => querry.OrderByDescending(p => p.Rating),
                "product" => querry.OrderBy(p => p.Product),
                _ => querry.OrderBy(p => p.User),
            };

            return querry;
        }
    }
}
