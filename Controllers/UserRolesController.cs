using Microsoft.AspNetCore.Mvc;
using SampleWebApi.DTOs;
using SampleWebApi.Services;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleResponse>>> GetUserRoles()
        {
            var userRoles = await _userRoleService.GetAllUserRolesAsync();
            return Ok(userRoles);
        }

        [HttpGet("{userId}/{roleId}")]
        public async Task<ActionResult<UserRoleResponse>> GetUserRole(int userId, int roleId)
        {
            var userRole = await _userRoleService.GetUserRoleByIdAsync(userId, roleId);
            if (userRole == null)
            {
                return NotFound();
            }
            return Ok(userRole);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserRoleResponse>>> GetUserRolesByUser(int userId)
        {
            var userRoles = await _userRoleService.GetUserRolesByUserIdAsync(userId);
            return Ok(userRoles);
        }

        [HttpGet("role/{roleId}")]
        public async Task<ActionResult<IEnumerable<UserRoleResponse>>> GetUserRolesByRole(int roleId)
        {
            var userRoles = await _userRoleService.GetUserRolesByRoleIdAsync(roleId);
            return Ok(userRoles);
        }

        [HttpPost]
        public async Task<ActionResult<UserRoleResponse>> CreateUserRole(UserRoleCreateRequest request)
        {
            try
            {
                var userRole = await _userRoleService.CreateUserRoleAsync(request);
                return CreatedAtAction(nameof(GetUserRole), new { userId = userRole.UserId, roleId = userRole.RoleId }, userRole);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}/{roleId}")]
        public async Task<IActionResult> DeleteUserRole(int userId, int roleId)
        {
            var result = await _userRoleService.DeleteUserRoleAsync(userId, roleId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
