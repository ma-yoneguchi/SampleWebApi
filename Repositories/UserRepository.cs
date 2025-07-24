using Microsoft.EntityFrameworkCore;
using SampleWebApi.Data;
using SampleWebApi.Entities;

namespace SampleWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .OrderBy(u => u.Department)
                .ThenBy(u => u.EmployeeCode)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByDepartmentAndEmployeeCodeAsync(string department, string employeeCode)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Department == department && u.EmployeeCode == employeeCode);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdWithOrdersAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Orders)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetByDepartmentAsync(string department)
        {
            return await _context.Users
                .Where(u => u.Department == department)
                .OrderBy(u => u.EmployeeCode)
                .ToListAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> ExistsByDepartmentAndEmployeeCodeAsync(string department, string employeeCode, int? excludeId = null)
        {
            var query = _context.Users
                .Where(u => u.Department == department && u.EmployeeCode == employeeCode);

            if (excludeId.HasValue)
            {
                query = query.Where(u => u.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email, int? excludeId = null)
        {
            var query = _context.Users
                .Where(u => u.Email == email);

            if (excludeId.HasValue)
            {
                query = query.Where(u => u.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }

        public async Task<bool> CanDeleteAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Orders)
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return false;

            return user.Orders.Count == 0 && user.UserRoles.Count == 0;
        }
    }
}
