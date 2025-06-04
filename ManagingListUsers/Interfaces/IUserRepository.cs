using ManagingListUsers.Dto;
using ManagingListUsers.Models;

namespace ManagingListUsers.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Save();
        Task<bool> UserExistsByIdAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserWithRolesByIdAsync(int userId);
        Task<bool> CheckUniqueEmailAsync(string email);
        Task<bool> CreateUserAsync(User user, int roleId);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> AddUserRoleAsync(int userId, int roleId);
        Task<(ICollection<User> users, int totalCount)> GetUsersPaginatedAsync(int page, int pageSize);
    }
}
