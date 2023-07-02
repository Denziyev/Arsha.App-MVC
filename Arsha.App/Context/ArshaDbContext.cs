using Arsha.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Context
{
    public class ArshaDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public ArshaDbContext(DbContextOptions<ArshaDbContext> options) : base(options)
        {

        }
    }
}
