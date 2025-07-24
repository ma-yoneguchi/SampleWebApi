using SampleWebApi.DTOs;

namespace SampleWebApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductResponse>> GetProductsByCategoryIdAsync(int categoryId);
        Task<ProductResponse> CreateProductAsync(ProductCreateRequest request);
        Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest request);
        Task<bool> DeleteProductAsync(int id);
    }
}
