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

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get users by pagination
        /// </summary>
        /// <returns>Page of users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("Pagination/{page}/{pageSize}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersByPagination(int page, int pageSize)
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersByPagination(page, pageSize));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns>User</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        /// <response code="404">Not found user</response>
        [HttpGet("Id/{id}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int id)
        {
            if (!_userRepository.UserExistsById(id))
                return NotFound();
            var user = _mapper.Map<UserDto>(_userRepository.GetUserById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        /// <summary>
        /// Get user by name
        /// </summary>
        /// <returns>User</returns>        
        /// <response code="200">Ok</response>
        [HttpGet("Name/{name}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByName(string name)
        {
            var user = _mapper.Map<List<UserDto>>(_userRepository.GetUserByName(name));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        /// <summary>
        /// Get user by age
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("Age/{age}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByAge(int age)
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUserByAge(age));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user by email
        /// </summary>
        /// <returns>User</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        /// <response code="404">Not found user</response>
        [HttpGet("Email/{email}")]
        [ProducesResponseType(200, Type = typeof(UserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUserByAge(string email)
        {
            if (!_userRepository.UserExistsByEmail(email))
                return NotFound();
            var users = _mapper.Map<UserDto>(_userRepository.GetUserByEmail(email));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user order by id
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderById")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public IActionResult GetUsersOrderById()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersOrderById());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user order by Name
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByName")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersOrderByName()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersOrderByName());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user order by Age
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByAge")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersOrderByAge()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersOrderByAge());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user order by Email
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByEmail")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersOrderByEmail()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersOrderByEmail());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user order descending by id
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByIdDescending")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersOrderByIdDescending()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersOrderByIdDescending());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user order descending by Name
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByNameDescending")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersOrderByNameDescending()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersOrderByNameDescending());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user order descending by Age
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByAgeDescending")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersOrderByAgeDescending()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersOrderByAgeDescending());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get user order descending by Email
        /// </summary>
        /// <returns>Users</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByEmailDescending")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUsersOrderByEmailDescending()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersOrderByEmailDescending());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{userId}/Roles")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserWithRoles(int userId)
        {
            if (!_userRepository.UserExistsById(userId))
                return NotFound();
            var users = _mapper.Map<User>(_userRepository.GetUserWithRolesById(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Create user
        /// </summary>
        /// <remarks>
        /// Name is required, Unique Email, Age must be between 1 and 120
        /// </remarks>
        /// <param name="userCreate">Model user</param>
        /// <param name="roleId">Role id</param>
        /// <returns>New user create</returns>
        /// <response code="200">Successfully created</response>
        /// <response code="400">Model invalid</response>
        /// <response code="422">Email is not unique</response>
        /// <response code="500">Something went wrong while saving</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        public IActionResult CreateUser([FromBody] UserDto userCreate, [FromQuery] int roleId)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!_userRepository.CheckUniqueEmail(userCreate.Email))
            {
                ModelState.AddModelError("", "Email is not unique");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap, roleId))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        /// <summary>
        /// Update user by id
        /// </summary>
        /// <remarks>
        /// Name is required, Unique Email, Age must be between 1 and 120
        /// </remarks>
        /// <param name="userId">User id</param>
        /// <param name="updatedUser">Model user</param>
        /// <returns>User update</returns>
        /// <response code="204">Successfully update</response>
        /// <response code="400">Model invalid</response>
        /// <response code="422">Email is not unique</response>
        /// <response code="500">Something went wrong updating user</response>
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userId != updatedUser.Id)
                return BadRequest(ModelState);

            if (!_userRepository.CheckUniqueEmail(updatedUser.Email))
            {
                ModelState.AddModelError("", "Email is not unique");
                return StatusCode(422, ModelState);
            }

            if (!_userRepository.UserExistsById(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User delete</returns>
        /// <response code="204">User is delete</response>
        /// <response code="400">Model invalid</response>
        /// <response code="404">Not found user</response>
        /// <response code="500">Something went wrong deleting user</response>
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExistsById(userId))
                return NotFound();

            var userToDelete = _userRepository.GetUserById(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        /// <summary>
        /// Add user role
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roleId">User id</param>
        /// <returns>User role add</returns>
        /// <response code="200">Successfully add</response>
        /// <response code="400">Model invalid</response>
        /// <response code="404">Not found user</response>
        /// <response code="500">Something went wrong add role</response>
        [HttpPost("AddUserRole")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult AddUserRole([FromQuery] int userId, [FromQuery] int roleId)
        {
            if (!_userRepository.UserExistsById(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_userRepository.AddUserRole(userId, roleId))
            {
                ModelState.AddModelError("", "Something went wrong add role");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully add");
        }
    }
}
