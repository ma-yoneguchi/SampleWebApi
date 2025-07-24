using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DTOs
{
    public class OrderCreateRequest
    {
        [Required(ErrorMessage = "注文番号は必須です")]
        [StringLength(20, ErrorMessage = "注文番号は20文字以内で入力してください")]
        public required string OrderNumber { get; set; }

        [Required(ErrorMessage = "注文タイプは必須です")]
        [StringLength(10, ErrorMessage = "注文タイプは10文字以内で入力してください")]
        public required string OrderType { get; set; }

        [Required(ErrorMessage = "注文日は必須です")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "金額は必須です")]
        [Range(0.01, double.MaxValue, ErrorMessage = "金額は0より大きい値を入力してください")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "ユーザーIDは必須です")]
        public int UserId { get; set; }

        public int? CategoryId { get; set; }  // 追加
    }

    public class OrderUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "注文番号は必須です")]
        [StringLength(20, ErrorMessage = "注文番号は20文字以内で入力してください")]
        public required string OrderNumber { get; set; }

        [Required(ErrorMessage = "注文タイプは必須です")]
        [StringLength(10, ErrorMessage = "注文タイプは10文字以内で入力してください")]
        public required string OrderType { get; set; }

        [Required(ErrorMessage = "注文日は必須です")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "金額は必須です")]
        [Range(0.01, double.MaxValue, ErrorMessage = "金額は0より大きい値を入力してください")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "ユーザーIDは必須です")]
        public int UserId { get; set; }

        public int? CategoryId { get; set; }  // 追加
    }

    public class OrderResponse
    {
        public int Id { get; set; }
        public required string OrderNumber { get; set; }
        public required string OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int? CategoryId { get; set; }  // 追加
        public string? UserName { get; set; }
        public string? CategoryName { get; set; }  // 追加
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
