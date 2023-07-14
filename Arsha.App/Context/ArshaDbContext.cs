using Arsha.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arsha.App.Context
{
    public class ArshaDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public ArshaDbContext(DbContextOptions<ArshaDbContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>(a =>
        //    {
        //        a.Property<int>("CategoryId");
        //        a.HasKey("Id");
        //    });

        //    base.OnModelCreating(modelBuilder);
        //}


    }
}
