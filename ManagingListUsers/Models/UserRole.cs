using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagingListUsers.Models
{
    /// <summary>
    /// Dependency of many-to-many tables User and Role
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// User id 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Role id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Role
        /// </summary>
        public Role Role { get; set; }
    }
}