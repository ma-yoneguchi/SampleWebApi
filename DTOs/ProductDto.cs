using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DTOs
{
    public class ProductCreateRequest
    {
        [Required(ErrorMessage = "商品コードは必須です")]
        [StringLength(30, ErrorMessage = "商品コードは30文字以内で入力してください")]
        public required string ProductCode { get; set; }

        [Required(ErrorMessage = "商品バージョンは必須です")]
        [StringLength(10, ErrorMessage = "商品バージョンは10文字以内で入力してください")]
        public required string ProductVersion { get; set; }

        [Required(ErrorMessage = "商品名は必須です")]
        [StringLength(200, ErrorMessage = "商品名は200文字以内で入力してください")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "価格は必須です")]
        [Range(0.01, double.MaxValue, ErrorMessage = "価格は0より大きい値を入力してください")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "カテゴリIDは必須です")]
        public int CategoryId { get; set; }
    }

    public class ProductUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "商品コードは必須です")]
        [StringLength(30, ErrorMessage = "商品コードは30文字以内で入力してください")]
        public required string ProductCode { get; set; }

        [Required(ErrorMessage = "商品バージョンは必須です")]
        [StringLength(10, ErrorMessage = "商品バージョンは10文字以内で入力してください")]
        public required string ProductVersion { get; set; }

        [Required(ErrorMessage = "商品名は必須です")]
        [StringLength(200, ErrorMessage = "商品名は200文字以内で入力してください")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "価格は必須です")]
        [Range(0.01, double.MaxValue, ErrorMessage = "価格は0より大きい値を入力してください")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "カテゴリIDは必須です")]
        public int CategoryId { get; set; }
    }

    public class ProductResponse
    {
        public int Id { get; set; }
        public required string ProductCode { get; set; }
        public required string ProductVersion { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
