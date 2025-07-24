using SampleWebApi.DTOs;

namespace SampleWebApi.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetAllOrdersAsync();
        Task<OrderResponse?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderResponse>> GetOrdersByUserIdAsync(int userId);
        Task<OrderResponse> CreateOrderAsync(OrderCreateRequest request);
        Task<OrderResponse> UpdateOrderAsync(OrderUpdateRequest request);
        Task<bool> DeleteOrderAsync(int id);
    }
}
