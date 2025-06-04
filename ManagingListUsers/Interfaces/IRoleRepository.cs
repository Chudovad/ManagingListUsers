using ManagingListUsers.Models;

namespace ManagingListUsers.Interfaces
{
    public interface IRoleRepository
    {
        Task<(IEnumerable<Role> Roles, int TotalCount)> GetRolesPaginatedAsync(
            int page, int pageSize, string sortBy, bool descending);
        Task<Role> GetRoleByIdAsync(int id);
        Task<bool> RoleExistsByIdAsync(int id);
        Task<bool> RoleExistsByNameAsync(string name);
        Task<bool> RoleNameExistsForOtherRoleAsync(int roleId, string name);
        Task<bool> CreateRoleAsync(Role role);
        Task<bool> UpdateRoleAsync(Role role);
        Task<bool> DeleteRoleAsync(int id);
    }
}