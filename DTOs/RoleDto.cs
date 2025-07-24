using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DTOs
{
    public class RoleCreateRequest
    {
        [Required(ErrorMessage = "ロール名は必須です")]
        [StringLength(50, ErrorMessage = "ロール名は50文字以内で入力してください")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "説明は200文字以内で入力してください")]
        public string? Description { get; set; }
    }

    public class RoleUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "ロール名は必須です")]
        [StringLength(50, ErrorMessage = "ロール名は50文字以内で入力してください")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "説明は200文字以内で入力してください")]
        public string? Description { get; set; }
    }

    public class RoleResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
