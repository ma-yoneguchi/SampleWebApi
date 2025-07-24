using Microsoft.EntityFrameworkCore;
using SampleWebApi.Data;
using SampleWebApi.Entities;

namespace SampleWebApi.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int orderId, int productId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Product)
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetByProductIdAsync(int productId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Where(oi => oi.ProductId == productId)
                .ToListAsync();
        }

        public async Task<OrderItem> CreateAsync(OrderItem orderItem)
        {
            orderItem.CreatedAt = DateTime.UtcNow;
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItem> UpdateAsync(OrderItem orderItem)
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;

            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task<bool> DeleteAsync(int orderId, int productId)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);

            if (orderItem == null) return false;

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int orderId, int productId)
        {
            return await _context.OrderItems
                .AnyAsync(oi => oi.OrderId == orderId && oi.ProductId == productId);
        }
    }
}
