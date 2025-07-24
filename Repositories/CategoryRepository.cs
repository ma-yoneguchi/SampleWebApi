using Microsoft.EntityFrameworkCore;
using SampleWebApi.Data;
using SampleWebApi.Entities;

namespace SampleWebApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .OrderBy(c => c.CategoryCode)
                .ThenBy(c => c.CategoryType)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByCategoryCodeAndTypeAsync(string categoryCode, string categoryType)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryCode == categoryCode && c.CategoryType == categoryType);
        }

        public async Task<Category?> GetByIdWithChildrenAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.ChildCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            category.CreatedAt = DateTime.UtcNow;
            category.UpdatedAt = DateTime.UtcNow;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            category.UpdatedAt = DateTime.UtcNow;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsByCategoryCodeAndTypeAsync(string categoryCode, string categoryType, int? excludeId = null)
        {
            var query = _context.Categories
                .Where(c => c.CategoryCode == categoryCode && c.CategoryType == categoryType);

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> CanDeleteAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.ChildCategories)
                .Include(c => c.Orders)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return false;

            return category.ChildCategories.Count == 0 &&
                   category.Orders.Count == 0 &&
                   category.Products.Count == 0;
        }
    }
}
