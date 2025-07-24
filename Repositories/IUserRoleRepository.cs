using SampleWebApi.Entities;

namespace SampleWebApi.Repositories
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<UserRole?> GetByIdAsync(int userId, int roleId);
        Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetByRoleIdAsync(int roleId);
        Task<UserRole> CreateAsync(UserRole userRole);
        Task<bool> DeleteAsync(int userId, int roleId);
        Task<bool> ExistsAsync(int userId, int roleId);
    }
}
