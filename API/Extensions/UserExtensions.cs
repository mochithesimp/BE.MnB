using API.Entities;

namespace API.Extensions
{
    public static class UserExtensions
    {

        public static IQueryable<User> SearchName(this IQueryable<User> querry, string? searchName)
        {
            if (string.IsNullOrEmpty(searchName)) return querry;

            var lowerCaseSearchTerm = searchName.Trim().ToLower();

            return querry.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<User> SearchEmail(this IQueryable<User> querry, string? searchEmail)
        {
            if (string.IsNullOrEmpty(searchEmail)) return querry;

            var lowerCaseSearchTerm = searchEmail.Trim().ToLower();

            return querry.Where(p => p.Email.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<User> FilterRole(this IQueryable<User> querry, int roleId)
        {

            querry = querry.Where(p => roleId == 0 || p.RoleId == roleId);

            return querry;
        }

        public static IQueryable<User> Sort(this IQueryable<User> querry, string? orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return querry;

            querry = orderBy switch
            {
                "name" => querry.OrderBy(p => p.Name),
                "email" => querry.OrderBy(p => p.Email),
                "id" => querry.OrderBy(p => p.UserId),
                "address" => querry.OrderBy(p => p.Address),
                "role" => querry.OrderBy(p => p.RoleId),
            };

            return querry;
        }

    }
}
