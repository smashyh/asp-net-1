using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class SignInViewModel
    {
        [Display(Name = "E-mail", Prompt = "Enter your e-mail.")]
        [Required(ErrorMessage = "A valid e-mail is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Display(Name = "Password", Prompt = "Enter your password.")]
        [Required(ErrorMessage = "A valid password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
