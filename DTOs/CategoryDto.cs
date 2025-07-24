using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DTOs
{
    public class CategoryCreateRequest
    {
        [Required(ErrorMessage = "カテゴリコードは必須です")]
        [StringLength(20, ErrorMessage = "カテゴリコードは20文字以内で入力してください")]
        public required string CategoryCode { get; set; }

        [Required(ErrorMessage = "カテゴリタイプは必須です")]
        [StringLength(10, ErrorMessage = "カテゴリタイプは10文字以内で入力してください")]
        public required string CategoryType { get; set; }

        [Required(ErrorMessage = "カテゴリ名は必須です")]
        [StringLength(100, ErrorMessage = "カテゴリ名は100文字以内で入力してください")]
        public required string Name { get; set; }

        [StringLength(500, ErrorMessage = "説明は500文字以内で入力してください")]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }

    public class CategoryUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "カテゴリコードは必須です")]
        [StringLength(20, ErrorMessage = "カテゴリコードは20文字以内で入力してください")]
        public required string CategoryCode { get; set; }

        [Required(ErrorMessage = "カテゴリタイプは必須です")]
        [StringLength(10, ErrorMessage = "カテゴリタイプは10文字以内で入力してください")]
        public required string CategoryType { get; set; }

        [Required(ErrorMessage = "カテゴリ名は必須です")]
        [StringLength(100, ErrorMessage = "カテゴリ名は100文字以内で入力してください")]
        public required string Name { get; set; }

        [StringLength(500, ErrorMessage = "説明は500文字以内で入力してください")]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }

    public class CategoryResponse
    {
        public int Id { get; set; }
        public required string CategoryCode { get; set; }
        public required string CategoryType { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? ParentCategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
