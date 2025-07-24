using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DTOs
{
    public class UserRoleCreateRequest
    {
        [Required(ErrorMessage = "ユーザーIDは必須です")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "ロールIDは必須です")]
        public int RoleId { get; set; }
    }

    public class UserRoleResponse
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
