using System.ComponentModel.DataAnnotations;
using WebApp.Statics;

namespace WebApp.Models;

public class AccountSecurityChangePasswordViewModel
{
    //[Required(ErrorMessage = "You must enter a password.")]
    //[RegularExpression(RegExStrings.PasswordRegEx, ErrorMessage = "A valid password is required.")]
    //[MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [Display(Name = "Current password", Prompt = "Enter your password.")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = null!;

    [Display(Name = "New password", Prompt = "Enter your password.")]
    [Required(ErrorMessage = "You must enter a password.")]
    [RegularExpression(RegExStrings.PasswordRegEx, ErrorMessage = "A valid password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "Confirm password", Prompt = "Confirm your password.")]
    [Required(ErrorMessage = "Please confirm your password.")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match each other.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}
