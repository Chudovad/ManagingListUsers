using System.ComponentModel.DataAnnotations;

namespace ManagingListUsers.Dto
{
    /// <summary>
    /// Role Data Transfer Object
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Role Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Role name is required
        /// </summary>
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }
    }
}
