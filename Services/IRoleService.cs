using SampleWebApi.DTOs;

namespace SampleWebApi.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllRolesAsync();
        Task<RoleResponse?> GetRoleByIdAsync(int id);
        Task<RoleResponse> CreateRoleAsync(RoleCreateRequest request);
        Task<RoleResponse> UpdateRoleAsync(RoleUpdateRequest request);
        Task<bool> DeleteRoleAsync(int id);
    }
}
