using Microsoft.AspNetCore.Mvc;
using UserRoleManagementApi.Models;
using UserRoleManagementApi.Services.Interfaces;

namespace UserRoleManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            return Ok(await _roleService.GetAllRoles());
        }

        // GET: api/roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // POST: api/roles
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            var createdRole = await _roleService.CreateRole(role);
            return CreatedAtAction(nameof(GetRole), new { id = createdRole.Id }, createdRole);
        }

        // PUT: api/roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }
            await _roleService.UpdateRole(role);
            return NoContent();
        }

        // DELETE: api/roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleService.DeleteRole(id);
            return NoContent();
        }

        // POST: api/roles/assign
        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] RoleAssignment assignment)
        {
            try
            {
                await _roleService.AssignRoleToUser(assignment.UserId, assignment.RoleId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/roles/remove
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] RoleAssignment assignment)
        {
            await _roleService.RemoveRoleFromUser(assignment.UserId, assignment.RoleId);
            return Ok();
        }
    }

    public class RoleAssignment
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}