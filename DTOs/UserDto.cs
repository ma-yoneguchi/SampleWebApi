using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DTOs
{
    public class UserCreateRequest
    {
        [Required(ErrorMessage = "ユーザー名は必須です")]
        [StringLength(100, ErrorMessage = "ユーザー名は100文字以内で入力してください")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "メールアドレスは必須です")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください")]
        [StringLength(200, ErrorMessage = "メールアドレスは200文字以内で入力してください")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "部署は必須です")]
        [StringLength(50, ErrorMessage = "部署は50文字以内で入力してください")]
        public required string Department { get; set; }

        [Required(ErrorMessage = "従業員コードは必須です")]
        [StringLength(20, ErrorMessage = "従業員コードは20文字以内で入力してください")]
        public required string EmployeeCode { get; set; }
    }

    public class UserUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "ユーザー名は必須です")]
        [StringLength(100, ErrorMessage = "ユーザー名は100文字以内で入力してください")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "メールアドレスは必須です")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください")]
        [StringLength(200, ErrorMessage = "メールアドレスは200文字以内で入力してください")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "部署は必須です")]
        [StringLength(50, ErrorMessage = "部署は50文字以内で入力してください")]
        public required string Department { get; set; }

        [Required(ErrorMessage = "従業員コードは必須です")]
        [StringLength(20, ErrorMessage = "従業員コードは20文字以内で入力してください")]
        public required string EmployeeCode { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Department { get; set; }
        public required string EmployeeCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class OrderSummaryResponse
    {
        public int Id { get; set; }
        public required string OrderNumber { get; set; }
        public required string OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
    }
}
