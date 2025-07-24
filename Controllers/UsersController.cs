using Microsoft.AspNetCore.Mvc;
using SampleWebApi.DTOs;
using SampleWebApi.Services;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "内部サーバーエラーが発生しました。", type = "SystemError" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "ユーザーが見つかりません。", type = "NotFoundError" });
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "内部サーバーエラーが発生しました。", type = "SystemError" });
            }
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult<UserResponse>> GetUserDetail(int id)
        {
            try
            {
                var user = await _userService.GetUserDetailByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "ユーザーが見つかりません。", type = "NotFoundError" });
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "内部サーバーエラーが発生しました。", type = "SystemError" });
            }
        }

        [HttpGet("department/{department}")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersByDepartment(string department)
        {
            try
            {
                var users = await _userService.GetUsersByDepartmentAsync(department);
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "内部サーバーエラーが発生しました。", type = "SystemError" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser(UserCreateRequest request)
        {
            try
            {
                var user = await _userService.CreateUserAsync(request);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (InvalidOperationException invalidOpEx)
            {
                return BadRequest(new { message = invalidOpEx.Message, type = "BusinessError" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "内部サーバーエラーが発生しました。", type = "SystemError" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> UpdateUser(int id, UserUpdateRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest(new { message = "IDが一致しません。", type = "ValidationError" });
            }

            try
            {
                var user = await _userService.UpdateUserAsync(request);
                return Ok(user);
            }
            catch (ArgumentException argEx)
            {
                return NotFound(new { message = argEx.Message, type = "NotFoundError" });
            }
            catch (InvalidOperationException invalidOpEx)
            {
                return BadRequest(new { message = invalidOpEx.Message, type = "BusinessError" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "内部サーバーエラーが発生しました。", type = "SystemError" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "ユーザーが見つかりません。", type = "NotFoundError" });
                }
                return NoContent();
            }
            catch (InvalidOperationException invalidOpEx)
            {
                return BadRequest(new { message = invalidOpEx.Message, type = "BusinessError" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "内部サーバーエラーが発生しました。", type = "SystemError" });
            }
        }

        [HttpPost("validate-unique")]
        public async Task<ActionResult<bool>> ValidateUniqueConstraint(
            [FromQuery] string department,
            [FromQuery] string employeeCode,
            [FromQuery] int? excludeId = null)
        {
            try
            {
                var isValid = await _userService.ValidateUniqueConstraintAsync(department, employeeCode, excludeId);
                return Ok(new { isValid, message = isValid ? "利用可能です" : "既に使用されています" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "内部サーバーエラーが発生しました。", type = "SystemError" });
            }
        }
    }
}
