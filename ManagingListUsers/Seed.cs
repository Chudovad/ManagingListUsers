using ManagingListUsers.Data;
using ManagingListUsers.Models;

namespace ManagingListUsers
{
    public class Seed
    {
        private readonly ApplicationDbContext _context;
        public Seed(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SeedDataContext()
        {
            if (!_context.UserRoles.Any())
            {
                var userRole = new List<UserRole>()
                {
                    new UserRole()
                    {
                        User = new User()
                        {
                            Name = "TestUser",
                            Age = 1,
                            Email = "test_user@test.com"
                        },
                        Role = new Role()
                        {
                            RoleName = "User",
                        }
                    },
                    new UserRole()
                    {
                        User = new User()
                        {
                            Name = "TestAdmin",
                            Age = 1,
                            Email = "test_admin@test.com"
                        },
                        Role = new Role()
                        {
                            RoleName = "Admin",
                        }
                    },
                    new UserRole()
                    {
                        User = new User()
                        {
                            Name = "TestSupport",
                            Age = 1,
                            Email = "test_support@test.com"
                        },
                        Role = new Role()
                        {
                            RoleName = "Support",
                        }
                    },
                    new UserRole()
                    {
                        User = new User()
                        {
                            Name = "TestSuperAdmin",
                            Age = 1,
                            Email = "test_superAdmin@test.com"
                        },
                        Role = new Role()
                        {
                            RoleName = "SuperAdmin",
                        }
                    }
                };
                _context.UserRoles.AddRange(userRole);
                _context.SaveChanges();
            }
        }

    }
}
