using SampleWebApi.Entities;

namespace SampleWebApi.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category?> GetByCategoryCodeAndTypeAsync(string categoryCode, string categoryType);
        Task<Category?> GetByIdWithChildrenAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByCategoryCodeAndTypeAsync(string categoryCode, string categoryType, int? excludeId = null);
        Task<bool> CanDeleteAsync(int id);
    }
}
