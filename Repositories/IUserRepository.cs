using SampleWebApi.Entities;

namespace SampleWebApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByDepartmentAndEmployeeCodeAsync(string department, string employeeCode);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdWithOrdersAsync(int id);
        Task<IEnumerable<User>> GetByDepartmentAsync(string department);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByDepartmentAndEmployeeCodeAsync(string department, string employeeCode, int? excludeId = null);
        Task<bool> ExistsByEmailAsync(string email, int? excludeId = null);
        Task<bool> CanDeleteAsync(int id);
    }
}
