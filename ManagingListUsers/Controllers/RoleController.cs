using AutoMapper;
using ManagingListUsers.Dto;
using ManagingListUsers.Interfaces;
using ManagingListUsers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagingListUsers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private const int DefaultPageSize = 10;
        private const int MaxPageSize = 50;

        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all roles with pagination and sorting
        /// </summary>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 10, max: 50)</param>
        /// <param name="sortBy">Field to sort by (id, name)</param>
        /// <param name="descending">Sort descending (default: false)</param>
        /// <returns>Paginated list of roles</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(PaginatedResponse<RoleDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRoles(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = DefaultPageSize,
            [FromQuery] string sortBy = "id",
            [FromQuery] bool descending = false)
        {
            if (page < 1 || pageSize < 1 || pageSize > MaxPageSize)
                return BadRequest("Invalid pagination parameters");

            var (roles, totalCount) = await _roleRepository.GetRolesPaginatedAsync(
                page, pageSize, sortBy, descending);

            var response = new PaginatedResponse<RoleDto>
            {
                Data = _mapper.Map<List<RoleDto>>(roles),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return Ok(response);
        }

        /// <summary>
        /// Get role by ID
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <returns>Role details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(RoleDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetRoleById(int id)
        {
            if (!await _roleRepository.RoleExistsByIdAsync(id))
                return NotFound();

            var role = await _roleRepository.GetRoleByIdAsync(id);
            return Ok(_mapper.Map<RoleDto>(role));
        }

        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="roleCreate">Role data</param>
        /// <returns>Created role</returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(RoleDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto roleCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _roleRepository.RoleExistsByNameAsync(roleCreate.RoleName))
            {
                ModelState.AddModelError("Name", "Role name already exists");
                return UnprocessableEntity(ModelState);
            }

            var role = _mapper.Map<Role>(roleCreate);

            if (!await _roleRepository.CreateRoleAsync(role))
            {
                ModelState.AddModelError("", "Failed to create role");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetRoleById), new { id = role.Id }, _mapper.Map<RoleDto>(role));
        }

        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <param name="roleUpdate">Updated role data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto roleUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != roleUpdate.Id)
                return BadRequest("ID mismatch");

            if (!await _roleRepository.RoleExistsByIdAsync(id))
                return NotFound();

            if (await _roleRepository.RoleNameExistsForOtherRoleAsync(id, roleUpdate.RoleName))
            {
                ModelState.AddModelError("Name", "Role name already exists");
                return UnprocessableEntity(ModelState);
            }

            var role = _mapper.Map<Role>(roleUpdate);

            if (!await _roleRepository.UpdateRoleAsync(role))
            {
                ModelState.AddModelError("", "Failed to update role");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (!await _roleRepository.RoleExistsByIdAsync(id))
                return NotFound();

            if (!await _roleRepository.DeleteRoleAsync(id))
            {
                ModelState.AddModelError("", "Failed to delete role");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}