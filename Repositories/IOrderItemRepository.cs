using SampleWebApi.Entities;

namespace SampleWebApi.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();
        Task<OrderItem?> GetByIdAsync(int orderId, int productId);
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItem>> GetByProductIdAsync(int productId);
        Task<OrderItem> CreateAsync(OrderItem orderItem);
        Task<OrderItem> UpdateAsync(OrderItem orderItem);
        Task<bool> DeleteAsync(int orderId, int productId);
        Task<bool> ExistsAsync(int orderId, int productId);
    }
}
