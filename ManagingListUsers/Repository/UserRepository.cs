using ManagingListUsers.Data;
using ManagingListUsers.Interfaces;
using ManagingListUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagingListUsers.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUserByAge(int age)
        {
            return _context.Users.Where(u => u.Age == age).ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<User> GetUserByName(string name)
        {
            return _context.Users.Where(u => u.Name == name).ToList();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public ICollection<User> GetUsersOrderById()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public ICollection<User> GetUsersOrderByEmail()
        {
            return _context.Users.OrderBy(u => u.Email).ToList();
        }

        public ICollection<User> GetUsersOrderByAge()
        {
            return _context.Users.OrderBy(u => u.Age).ToList();
        }

        public ICollection<User> GetUsersOrderByName()
        {
            return _context.Users.OrderBy(u => u.Name).ToList();
        }

        public ICollection<User> GetUsersOrderByIdDescending()
        {
            return _context.Users.OrderByDescending(u => u.Id).ToList();
        }

        public ICollection<User> GetUsersOrderByNameDescending()
        {
            return _context.Users.OrderByDescending(u => u.Name).ToList();
        }

        public ICollection<User> GetUsersOrderByAgeDescending()
        {
            return _context.Users.OrderByDescending(u => u.Age).ToList();
        }

        public ICollection<User> GetUsersOrderByEmailDescending()
        {
            return _context.Users.OrderByDescending(u => u.Email).ToList();
        }

        public bool UserExistsById(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public bool UserExistsByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public ICollection<User> GetUsersByPagination(int page, int pageSize)
        {
            return _context.Users.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public User GetUserWithRolesById(int userId)
        {
            return _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefault(u => u.Id == userId);
        }

        public bool CreateUser(User user, int roleId)
        {
            var userRoleEntity = _context.Roles.Where(r => r.Id == roleId).FirstOrDefault();
            if (userRoleEntity != null)
            {
                var userRole = new UserRole()
                {
                    User = user,
                    RoleId = roleId,
                };
                _context.Add(userRole);
                _context.Add(user);
                return Save();
            }
            return false;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User userUpdate)
        {
            _context.Users.Update(userUpdate);
            return Save();
        }

        public bool CheckUniqueEmail(string email)
        {
            var userEmail = _context.Users.FirstOrDefault(c => c.Email.Trim().ToUpper() == email.TrimEnd().ToUpper());

            return userEmail == null;
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public bool AddUserRole(int userId, int roleId)
        {
            var userRoleEntity = _context.Roles.Where(r => r.Id == roleId).FirstOrDefault();
            if (userRoleEntity != null)
            {
                var userRole = new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId,
                };
                _context.Add(userRole);
                return Save();
            }
            return false;
        }
    }
}
