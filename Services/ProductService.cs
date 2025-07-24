using AutoMapper;
using SampleWebApi.DTOs;
using SampleWebApi.Entities;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? _mapper.Map<ProductResponse>(product) : null;
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
        }

        public async Task<ProductResponse> CreateProductAsync(ProductCreateRequest request)
        {
            if (await _productRepository.ExistsByProductCodeAndVersionAsync(request.ProductCode, request.ProductVersion))
            {
                throw new InvalidOperationException($"商品コード'{request.ProductCode}'のバージョン'{request.ProductVersion}'は既に存在します。");
            }

            if (!await _categoryRepository.ExistsAsync(request.CategoryId))
            {
                throw new ArgumentException("指定されたカテゴリが存在しません。");
            }

            var product = _mapper.Map<Product>(request);
            var createdProduct = await _productRepository.CreateAsync(product);

            var productWithCategory = await _productRepository.GetByIdAsync(createdProduct.Id);
            return _mapper.Map<ProductResponse>(productWithCategory);
        }

        public async Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest request)
        {
            var existingProduct = await _productRepository.GetByIdAsync(request.Id);
            if (existingProduct == null)
            {
                throw new ArgumentException("指定された商品が見つかりません。");
            }

            if (await _productRepository.ExistsByProductCodeAndVersionAsync(request.ProductCode, request.ProductVersion, request.Id))
            {
                throw new InvalidOperationException($"商品コード'{request.ProductCode}'のバージョン'{request.ProductVersion}'は既に存在します。");
            }

            if (!await _categoryRepository.ExistsAsync(request.CategoryId))
            {
                throw new ArgumentException("指定されたカテゴリが存在しません。");
            }

            _mapper.Map(request, existingProduct);
            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);

            var productWithCategory = await _productRepository.GetByIdAsync(updatedProduct.Id);
            return _mapper.Map<ProductResponse>(productWithCategory);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            if (!await _productRepository.CanDeleteAsync(id))
            {
                throw new InvalidOperationException("関連する注文明細が存在するため削除できません。");
            }

            return await _productRepository.DeleteAsync(id);
        }
    }
}
