using AutoMapper;
using ManagingListUsers.Dto;
using ManagingListUsers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ManagingListUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        /// <returns>Roles</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetRoles()
        {
            var users = _mapper.Map<List<RoleDto>>(_roleRepository.GetRoles());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }

        /// <summary>
        /// Get role by id
        /// </summary>
        /// <returns>Role</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        /// <response code="404">Not found user</response>
        [HttpGet("Id/{id}")]
        [ProducesResponseType(200, Type = typeof(RoleDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetRoleById(int id)
        {
            if (!_roleRepository.RoleExistById(id))
                return NotFound();
            var role = _mapper.Map<RoleDto>(_roleRepository.GetRoleById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(role);
        }
        /// <summary>
        /// Get role by name
        /// </summary>
        /// <returns>Role</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        /// <response code="404">Not found user</response>
        [HttpGet("Name/{roleName}")]
        [ProducesResponseType(200, Type = typeof(RoleDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetRoleByName(string roleName)
        {
            if (!_roleRepository.RoleExistByName(roleName))
                return NotFound();
            var Role = _mapper.Map<RoleDto>(_roleRepository.GetRoleByName(roleName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(Role);
        }
        /// <summary>
        /// Get role order by id
        /// </summary>
        /// <returns>Roles</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderById")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetRolesOrderById()
        {
            var users = _mapper.Map<List<RoleDto>>(_roleRepository.GetRolesOrderById());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get role order by name
        /// </summary>
        /// <returns>Roles</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByName")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetRolesOrderByName()
        {
            var users = _mapper.Map<List<RoleDto>>(_roleRepository.GetRolesOrderByName());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get role order descending by id
        /// </summary>
        /// <returns>Roles</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByIdDescending")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetRolesOrderByIdDescending()
        {
            var users = _mapper.Map<List<RoleDto>>(_roleRepository.GetRolesOrderByIdDescending());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
        /// <summary>
        /// Get Role order descending by Name
        /// </summary>
        /// <returns>Roles</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Model invalid</response>
        [HttpGet("OrderByNameDescending")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RoleDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserOrderByNameDescending()
        {
            var users = _mapper.Map<List<RoleDto>>(_roleRepository.GetRolesOrderByNameDescending());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }
    }
}
