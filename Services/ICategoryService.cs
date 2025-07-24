using SampleWebApi.DTOs;

namespace SampleWebApi.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse?> GetCategoryByIdAsync(int id);
        Task<CategoryResponse> CreateCategoryAsync(CategoryCreateRequest request);
        Task<CategoryResponse> UpdateCategoryAsync(CategoryUpdateRequest request);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
