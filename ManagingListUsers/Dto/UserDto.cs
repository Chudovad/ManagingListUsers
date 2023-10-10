using System.ComponentModel.DataAnnotations;

namespace ManagingListUsers.Dto
{
    /// <summary>
    /// User Data Transfer Object
    /// </summary>
    public class UserDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Name required 
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        /// <summary>
        /// User age must be between 1 and 120(required)
        /// </summary>
        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }
        /// <summary>
        /// User's unique email(required)
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
