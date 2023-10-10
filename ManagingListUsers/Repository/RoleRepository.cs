using ManagingListUsers.Data;
using ManagingListUsers.Interfaces;
using ManagingListUsers.Models;

namespace ManagingListUsers.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Role GetRoleById(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }

        public Role GetRoleByName(string roleName)
        {
            return _context.Roles.FirstOrDefault(r => r.RoleName == roleName);
        }

        public ICollection<Role> GetRolesOrderByName()
        {
            return _context.Roles.OrderBy(r => r.RoleName).ToList();
        }

        public ICollection<Role> GetRolesOrderByNameDescending()
        {
            return _context.Roles.OrderByDescending(r => r.RoleName).ToList();
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public ICollection<Role> GetRolesOrderById()
        {
            return _context.Roles.OrderBy(r => r.Id).ToList();
        }

        public ICollection<Role> GetRolesOrderByIdDescending()
        {
            return _context.Roles.OrderByDescending(r => r.Id).ToList();
        }

        public bool RoleExistById(int id)
        {
            return _context.Roles.Any(r => r.Id == id);
        }

        public bool RoleExistByName(string roleName)
        {
            return _context.Roles.Any(r => r.RoleName == roleName);
        }
    }
}
