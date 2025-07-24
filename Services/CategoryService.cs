using AutoMapper;
using SampleWebApi.DTOs;
using SampleWebApi.Entities;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category != null ? _mapper.Map<CategoryResponse>(category) : null;
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryCreateRequest request)
        {
            if (await _categoryRepository.ExistsByCategoryCodeAndTypeAsync(request.CategoryCode, request.CategoryType))
            {
                throw new InvalidOperationException($"カテゴリコード'{request.CategoryCode}'のタイプ'{request.CategoryType}'は既に存在します。");
            }

            if (request.ParentCategoryId.HasValue && !await _categoryRepository.ExistsAsync(request.ParentCategoryId.Value))
            {
                throw new ArgumentException("指定された親カテゴリが存在しません。");
            }

            var category = _mapper.Map<Category>(request);
            var createdCategory = await _categoryRepository.CreateAsync(category);

            var categoryWithParent = await _categoryRepository.GetByIdAsync(createdCategory.Id);
            return _mapper.Map<CategoryResponse>(categoryWithParent);
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(CategoryUpdateRequest request)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(request.Id);
            if (existingCategory == null)
            {
                throw new ArgumentException("指定されたカテゴリが見つかりません。");
            }

            if (await _categoryRepository.ExistsByCategoryCodeAndTypeAsync(request.CategoryCode, request.CategoryType, request.Id))
            {
                throw new InvalidOperationException($"カテゴリコード'{request.CategoryCode}'のタイプ'{request.CategoryType}'は既に存在します。");
            }

            if (request.ParentCategoryId.HasValue && !await _categoryRepository.ExistsAsync(request.ParentCategoryId.Value))
            {
                throw new ArgumentException("指定された親カテゴリが存在しません。");
            }

            _mapper.Map(request, existingCategory);
            var updatedCategory = await _categoryRepository.UpdateAsync(existingCategory);

            var categoryWithParent = await _categoryRepository.GetByIdAsync(updatedCategory.Id);
            return _mapper.Map<CategoryResponse>(categoryWithParent);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            if (!await _categoryRepository.CanDeleteAsync(id))
            {
                throw new InvalidOperationException("関連するデータが存在するため削除できません。");
            }

            return await _categoryRepository.DeleteAsync(id);
        }
    }
}
