using AutoMapper;
using SampleWebApi.DTOs;
using SampleWebApi.Entities;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IUserRepository userRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<OrderResponse?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order != null ? _mapper.Map<OrderResponse>(order) : null;
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
        }

        public async Task<OrderResponse> CreateOrderAsync(OrderCreateRequest request)
        {
            // ユーザー存在チェック
            if (!await _userRepository.ExistsAsync(request.UserId))
            {
                throw new ArgumentException("指定されたユーザーが存在しません。");
            }

            // カテゴリ存在チェック（オプション）
            if (request.CategoryId.HasValue && !await _categoryRepository.ExistsAsync(request.CategoryId.Value))
            {
                throw new ArgumentException("指定されたカテゴリが存在しません。");
            }

            // 複合ユニーク制約チェック
            var existingOrder = await _orderRepository.GetByOrderNumberAndTypeAsync(request.OrderNumber, request.OrderType);
            if (existingOrder != null)
            {
                throw new InvalidOperationException($"注文番号'{request.OrderNumber}'のタイプ'{request.OrderType}'は既に存在します。");
            }

            var order = _mapper.Map<Order>(request);
            var createdOrder = await _orderRepository.CreateAsync(order);

            // 作成後にユーザー情報を含めて取得
            var orderWithUser = await _orderRepository.GetByIdAsync(createdOrder.Id);
            return _mapper.Map<OrderResponse>(orderWithUser);
        }

        public async Task<OrderResponse> UpdateOrderAsync(OrderUpdateRequest request)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(request.Id);
            if (existingOrder == null)
            {
                throw new ArgumentException("指定された注文が見つかりません。");
            }

            // ユーザー存在チェック
            if (!await _userRepository.ExistsAsync(request.UserId))
            {
                throw new ArgumentException("指定されたユーザーが存在しません。");
            }

            // カテゴリ存在チェック（オプション）
            if (request.CategoryId.HasValue && !await _categoryRepository.ExistsAsync(request.CategoryId.Value))
            {
                throw new ArgumentException("指定されたカテゴリが存在しません。");
            }

            // 複合ユニーク制約チェック（自分以外）
            var duplicateOrder = await _orderRepository.GetByOrderNumberAndTypeAsync(request.OrderNumber, request.OrderType);
            if (duplicateOrder != null && duplicateOrder.Id != request.Id)
            {
                throw new InvalidOperationException($"注文番号'{request.OrderNumber}'のタイプ'{request.OrderType}'は既に存在します。");
            }

            _mapper.Map(request, existingOrder);
            var updatedOrder = await _orderRepository.UpdateAsync(existingOrder);

            // 更新後にユーザー情報を含めて取得
            var orderWithUser = await _orderRepository.GetByIdAsync(updatedOrder.Id);
            return _mapper.Map<OrderResponse>(orderWithUser);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
    }
}
