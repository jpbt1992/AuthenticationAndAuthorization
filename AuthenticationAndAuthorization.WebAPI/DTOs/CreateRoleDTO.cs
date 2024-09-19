using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorization.WebAPI.DTOs
{
    public sealed class CreateRoleDTO
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; } = string.Empty;
    }
}
