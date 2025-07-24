using SampleWebApi.Entities;

namespace SampleWebApi.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> GetByProductCodeAndVersionAsync(string productCode, string productVersion);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByProductCodeAndVersionAsync(string productCode, string productVersion, int? excludeId = null);
        Task<bool> CanDeleteAsync(int id);
    }
}
