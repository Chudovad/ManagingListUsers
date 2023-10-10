using ManagingListUsers.Models;

namespace ManagingListUsers.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        bool UserExistsById(int id);
        bool UserExistsByEmail(string email);
        User GetUserById(int id);
        ICollection<User> GetUserByName(string name);
        ICollection<User> GetUserByAge(int age);
        User GetUserByEmail(string email);
        ICollection<User> GetUsersOrderById();
        ICollection<User> GetUsersOrderByName();
        ICollection<User> GetUsersOrderByAge();
        ICollection<User> GetUsersOrderByEmail();
        ICollection<User> GetUsersOrderByIdDescending();
        ICollection<User> GetUsersOrderByNameDescending();
        ICollection<User> GetUsersOrderByAgeDescending();
        ICollection<User> GetUsersOrderByEmailDescending();
        ICollection<User> GetUsersByPagination(int page, int pageSize);
        User GetUserWithRolesById(int userId);
        bool CreateUser(User User, int roleId);
        bool AddUserRole(int userId, int roleId);
        bool CheckUniqueEmail(string email);
        bool Save();
        bool UpdateUser(User user);
        bool DeleteUser(User user);
    }
}
