using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using WebApp.Statics;

namespace WebApp.Models;

public class AccountDetailsBasicInfoViewModel
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
    public string LastName { get; set;} = null!;

    [Display(Name = "E-mail", Prompt = "Enter your e-mail.")]
    [RegularExpression(RegExStrings.EmailRegEx, ErrorMessage = "A valid e-mail is required.")]
    [Required(ErrorMessage = "You must enter an e-mail address.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Phone", Prompt = "Enter your phone number.")]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Biography { get; set; }
}
