using ManagingListUsers.Models;

namespace ManagingListUsers.Interfaces
{
    public interface IRoleRepository
    {
        ICollection<Role> GetRoles();
        bool RoleExistById(int id);
        bool RoleExistByName(string roleName);
        Role GetRoleById(int id);
        Role GetRoleByName(string roleName);
        ICollection<Role> GetRolesOrderById();
        ICollection<Role> GetRolesOrderByName();
        ICollection<Role> GetRolesOrderByIdDescending();
        ICollection<Role> GetRolesOrderByNameDescending();
    }
}
