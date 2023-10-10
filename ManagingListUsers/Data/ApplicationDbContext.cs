using ManagingListUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagingListUsers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(us => us.UserRoles)
                .HasForeignKey(u => u.UserId);
            builder.Entity<UserRole>()
                .HasOne(u => u.Role)
                .WithMany(us => us.UserRoles)
                .HasForeignKey(u => u.RoleId);

            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


    }
}
