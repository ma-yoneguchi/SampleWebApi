using SampleWebApi.DTOs;

namespace SampleWebApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse?> GetUserByIdAsync(int id);
        Task<UserResponse?> GetUserDetailByIdAsync(int id);
        Task<IEnumerable<UserResponse>> GetUsersByDepartmentAsync(string department);
        Task<UserResponse> CreateUserAsync(UserCreateRequest request);
        Task<UserResponse> UpdateUserAsync(UserUpdateRequest request);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ValidateUniqueConstraintAsync(string department, string employeeCode, int? excludeId = null);
    }
}
