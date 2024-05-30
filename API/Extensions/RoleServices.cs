using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public interface IRoleService
    {
        Task<List<string>> GetRolesOfUser(int id);
    }

    public class RoleServices : IRoleService
    {

        private readonly StoreContext _context;

        public RoleServices(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetRolesOfUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new List<string>();
            }

            var roles = await _context.Roles
                .Where(r => r.RoleId == user.RoleId)
                .Select(r => r.RoleName)
                .ToListAsync();

            return roles;
        }
    }
}
