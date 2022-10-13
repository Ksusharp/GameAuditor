using System.ComponentModel.DataAnnotations;

namespace GameAuditor.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
