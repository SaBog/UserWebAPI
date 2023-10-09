using Microsoft.EntityFrameworkCore;
using UserWebAPI.Models;

namespace UserWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new { Id = 1, Name = "User" },
                new { Id = 2, Name = "Admin" },
                new { Id = 3, Name = "Support" },
                new { Id = 4, Name = "SuperAdmin" }
                );

        }
    }
}
