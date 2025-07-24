using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DTOs
{
    public class OrderItemCreateRequest
    {
        [Required(ErrorMessage = "注文IDは必須です")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "商品IDは必須です")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "数量は必須です")]
        [Range(1, int.MaxValue, ErrorMessage = "数量は1以上を入力してください")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "単価は必須です")]
        [Range(0.01, double.MaxValue, ErrorMessage = "単価は0より大きい値を入力してください")]
        public decimal UnitPrice { get; set; }
    }

    public class OrderItemResponse
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ProductName { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
