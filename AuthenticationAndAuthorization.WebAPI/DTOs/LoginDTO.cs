using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorization.WebAPI.DTOs
{
    public sealed class LoginDTO
    {
        [Display(Name = "User")]
        [Required(ErrorMessage = "Enter the name")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter the password")]
        public string Password { get; set; } = string.Empty;
    }
}
