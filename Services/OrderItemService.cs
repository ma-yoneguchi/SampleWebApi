using AutoMapper;
using SampleWebApi.DTOs;
using SampleWebApi.Entities;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderItemService(
            IOrderItemRepository orderItemRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemResponse>> GetAllOrderItemsAsync()
        {
            var orderItems = await _orderItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
        }

        public async Task<OrderItemResponse?> GetOrderItemByIdAsync(int orderId, int productId)
        {
            var orderItem = await _orderItemRepository.GetByIdAsync(orderId, productId);
            return orderItem != null ? _mapper.Map<OrderItemResponse>(orderItem) : null;
        }

        public async Task<IEnumerable<OrderItemResponse>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var orderItems = await _orderItemRepository.GetByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
        }

        public async Task<IEnumerable<OrderItemResponse>> GetOrderItemsByProductIdAsync(int productId)
        {
            var orderItems = await _orderItemRepository.GetByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
        }

        public async Task<OrderItemResponse> CreateOrderItemAsync(OrderItemCreateRequest request)
        {
            if (await _orderItemRepository.ExistsAsync(request.OrderId, request.ProductId))
            {
                throw new InvalidOperationException("同じ注文に同じ商品は既に存在します。");
            }

            if (!await _orderRepository.ExistsAsync(request.OrderId))
            {
                throw new ArgumentException("指定された注文が存在しません。");
            }

            if (!await _productRepository.ExistsAsync(request.ProductId))
            {
                throw new ArgumentException("指定された商品が存在しません。");
            }

            var orderItem = _mapper.Map<OrderItem>(request);
            var createdOrderItem = await _orderItemRepository.CreateAsync(orderItem);

            var orderItemWithRelations = await _orderItemRepository.GetByIdAsync(createdOrderItem.OrderId, createdOrderItem.ProductId);
            return _mapper.Map<OrderItemResponse>(orderItemWithRelations);
        }

        public async Task<bool> DeleteOrderItemAsync(int orderId, int productId)
        {
            return await _orderItemRepository.DeleteAsync(orderId, productId);
        }
    }
}
