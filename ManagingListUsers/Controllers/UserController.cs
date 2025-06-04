using AutoMapper;
using ManagingListUsers.Dto;
using ManagingListUsers.Interfaces;
using ManagingListUsers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagingListUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;
        private const int MaxPageSize = 50;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users with pagination
        /// </summary>
        /// <param name="page">Page number (starting from 1)</param>
        /// <param name="pageSize">Number of items per page (max 50)</param>
        /// <returns>Paginated list of users</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PaginatedResponse<UserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = DefaultPageSize)
        {
            if (page < 1 || pageSize < 1 || pageSize > MaxPageSize)
                return BadRequest("Invalid pagination parameters");

            var (users, totalCount) = await _userRepository.GetUsersPaginatedAsync(page, pageSize);

            if (users == null || !users.Any())
                return NotFound();

            var response = new PaginatedResponse<UserDto>
            {
                Data = _mapper.Map<List<UserDto>>(users),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return Ok(response);
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User information</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (!await _userRepository.UserExistsByIdAsync(id))
                return NotFound();

            var user = await _userRepository.GetUserByIdAsync(id);

            return Ok(_mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Get user roles
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>User with roles</returns>
        [HttpGet("{userId}/roles")]
        [ProducesResponseType(200, Type = typeof(UserWithRolesDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserWithRoles(int userId)
        {
            if (!await _userRepository.UserExistsByIdAsync(userId))
                return NotFound();

            var user = await _userRepository.GetUserWithRolesByIdAsync(userId);
            return Ok(_mapper.Map<UserWithRolesDto>(user));
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userCreate">User data</param>
        /// <param name="roleId">Initial role ID</param>
        /// <returns>Created user</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userCreate, [FromQuery] int roleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _userRepository.CheckUniqueEmailAsync(userCreate.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use");
                return UnprocessableEntity(ModelState);
            }

            var user = _mapper.Map<User>(userCreate);

            if (!await _userRepository.CreateUserAsync(user, roleId))
            {
                ModelState.AddModelError("", "Failed to create user");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, _mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="userUpdate">Updated user data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != userUpdate.Id)
                return BadRequest("ID mismatch");

            if (!await _userRepository.UserExistsByIdAsync(id))
                return NotFound();

            if (!await _userRepository.CheckUniqueEmailAsync(userUpdate.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use");
                return UnprocessableEntity(ModelState);
            }

            var user = _mapper.Map<User>(userUpdate);

            if (!await _userRepository.UpdateUserAsync(user))
            {
                ModelState.AddModelError("", "Failed to update user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!await _userRepository.UserExistsByIdAsync(id))
                return NotFound();

            if (!await _userRepository.DeleteUserAsync(id))
            {
                ModelState.AddModelError("", "Failed to delete user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="roleId">Role ID</param>
        /// <returns>No content</returns>
        [HttpPost("{userId}/roles/{roleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> AddUserRole(int userId, int roleId)
        {
            if (!await _userRepository.UserExistsByIdAsync(userId))
                return NotFound("User not found");

            if (!await _userRepository.AddUserRoleAsync(userId, roleId))
            {
                ModelState.AddModelError("", "Failed to add role to user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}