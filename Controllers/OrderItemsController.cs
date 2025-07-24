using Microsoft.AspNetCore.Mvc;
using SampleWebApi.DTOs;
using SampleWebApi.Services;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetOrderItems()
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItem(int orderId, int productId)
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(orderId, productId);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetOrderItemsByOrder(int orderId)
        {
            var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(orderItems);
        }

        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetOrderItemsByProduct(int productId)
        {
            var orderItems = await _orderItemService.GetOrderItemsByProductIdAsync(productId);
            return Ok(orderItems);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemResponse>> CreateOrderItem(OrderItemCreateRequest request)
        {
            try
            {
                var orderItem = await _orderItemService.CreateOrderItemAsync(request);
                return CreatedAtAction(nameof(GetOrderItem), new { orderId = orderItem.OrderId, productId = orderItem.ProductId }, orderItem);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderItem(int orderId, int productId)
        {
            var result = await _orderItemService.DeleteOrderItemAsync(orderId, productId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
