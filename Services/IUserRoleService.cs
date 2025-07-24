using SampleWebApi.DTOs;

namespace SampleWebApi.Services
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRoleResponse>> GetAllUserRolesAsync();
        Task<UserRoleResponse?> GetUserRoleByIdAsync(int userId, int roleId);
        Task<IEnumerable<UserRoleResponse>> GetUserRolesByUserIdAsync(int userId);
        Task<IEnumerable<UserRoleResponse>> GetUserRolesByRoleIdAsync(int roleId);
        Task<UserRoleResponse> CreateUserRoleAsync(UserRoleCreateRequest request);
        Task<bool> DeleteUserRoleAsync(int userId, int roleId);
    }
}
