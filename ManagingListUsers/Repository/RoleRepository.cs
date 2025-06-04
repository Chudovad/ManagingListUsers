using ManagingListUsers.Data;
using ManagingListUsers.Interfaces;
using ManagingListUsers.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagingListUsers.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Role> Roles, int TotalCount)> GetRolesPaginatedAsync(
            int page, int pageSize, string sortBy, bool descending)
        {
            var query = _context.Roles.AsQueryable();

            query = (sortBy.ToLower(), descending) switch
            {
                ("name", false) => query.OrderBy(r => r.RoleName),
                ("name", true) => query.OrderByDescending(r => r.RoleName),
                (_, false) => query.OrderBy(r => r.Id),
                (_, true) => query.OrderByDescending(r => r.Id)
            };

            var totalCount = await query.CountAsync();
            var roles = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (roles, totalCount);
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FirstAsync(r => r.Id == id);
        }

        public async Task<bool> RoleExistsByIdAsync(int id)
        {
            return await _context.Roles.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> RoleExistsByNameAsync(string name)
        {
            return await _context.Roles.AnyAsync(r => r.RoleName == name);
        }

        public async Task<bool> RoleNameExistsForOtherRoleAsync(int roleId, string name)
        {
            return await _context.Roles
                .AnyAsync(r => r.RoleName == name && r.Id != roleId);
        }

        public async Task<bool> CreateRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            return await SaveAsync();
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            return await SaveAsync();
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return false;

            _context.Roles.Remove(role);
            return await SaveAsync();
        }

        private async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}