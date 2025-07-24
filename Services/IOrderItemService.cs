using SampleWebApi.DTOs;

namespace SampleWebApi.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemResponse>> GetAllOrderItemsAsync();
        Task<OrderItemResponse?> GetOrderItemByIdAsync(int orderId, int productId);
        Task<IEnumerable<OrderItemResponse>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItemResponse>> GetOrderItemsByProductIdAsync(int productId);
        Task<OrderItemResponse> CreateOrderItemAsync(OrderItemCreateRequest request);
        Task<bool> DeleteOrderItemAsync(int orderId, int productId);
    }
}
