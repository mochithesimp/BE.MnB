using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ForAge> forAges { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImageProduct> imageProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
