using System.ComponentModel.DataAnnotations;
using WebApp.Statics;

namespace WebApp.Models
{
    public class SignUpViewModel
    {
        [Display(Name = "First name", Prompt = "Enter your first name.")]
        [Required(ErrorMessage = "A valid first name is required.")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = null!;
        
        [Display(Name = "Last name", Prompt = "Enter your last name.")]
        [Required(ErrorMessage = "A valid last name is required.")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = null!;
        
        [Display(Name = "E-mail", Prompt = "Enter your e-mail.")]
        [RegularExpression(RegExStrings.EmailRegEx, ErrorMessage = "A valid e-mail is required.")]
        [Required(ErrorMessage = "You must enter an e-mail address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Display(Name = "Password", Prompt = "Enter your password.")]
        [Required(ErrorMessage = "You must enter a password.")]
        [RegularExpression(RegExStrings.PasswordRegEx, ErrorMessage = "A valid password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirm password", Prompt = "Confirm your password.")]
        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match each other.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Display(Name = "I agree to the Terms & Conditions.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please agree to the Terms & Conditions.")]
        public bool TermsAndConditions { get; set; }
    }
}
