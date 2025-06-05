using System.Threading.Tasks;
using ManagingListUsers.Data;
using ManagingListUsers.Dto;
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

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateUserAsync(User userUpdate)
        {
            var existingUser = await GetUserByIdAsync(userUpdate.Id);
            if (existingUser == null) return false;

            _context.Entry(existingUser).CurrentValues.SetValues(userUpdate);

            return await Save();
        }

        public async Task<bool> UserExistsByIdAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<User> GetUserWithRolesByIdAsync(int userId)
        {
            return await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstAsync(u => u.Id == userId);
        }

        public async Task<bool> CheckUniqueEmailAsync(string email)
        {
            var userEmail = await _context.Users.FirstOrDefaultAsync(c => c.Email.Trim().ToUpper() == email.TrimEnd().ToUpper());

            return userEmail == null;
        }

        public async Task<bool> CreateUserAsync(User user, int roleId)
        {
            var userRoleEntity = await _context.Roles.Where(r => r.Id == roleId).FirstOrDefaultAsync();
            if (userRoleEntity != null)
            {
                var userRole = new UserRole()
                {
                    User = user,
                    RoleId = roleId,
                };
                _context.Add(user);
                return await Save();
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            _context.Remove(user);
            return await Save();
        }

        public async Task<bool> AddUserRoleAsync(int userId, int roleId)
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
                return await Save();
            }
            return false;
        }

        public async Task<(ICollection<User> users, int totalCount)> GetUsersPaginatedAsync(int page, int pageSize)
        {
            var users = await _context.Users.Skip((page - 1) * pageSize).Take(pageSize).OrderBy(u => u.Id).ToListAsync();

            return (users, await _context.Users.CountAsync());
        }
    }
}
