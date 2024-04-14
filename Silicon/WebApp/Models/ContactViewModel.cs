using System.ComponentModel.DataAnnotations;
using WebApp.Statics;

namespace WebApp.Models;

public class ContactViewModel
{
    [Display(Name = "Full Name", Prompt = "Enter your full name.")]
    [Required(ErrorMessage = "A valid name is required.")]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters.")]
    [DataType(DataType.Text)]
    public string FullName { get; set; } = null!;

    [Display(Name = "E-mail", Prompt = "Enter your e-mail.")]
    [RegularExpression(RegExStrings.EmailRegEx, ErrorMessage = "A valid e-mail is required.")]
    [Required(ErrorMessage = "A valid e-mail is required.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Service", Prompt = "Choose the service you are interested in.")]
    [Required(ErrorMessage = "A valid service is required.")]
    public string Service { get; set; } = null!;

    [Display(Name = "Message", Prompt = "Enter your message here.")]
    [Required(ErrorMessage = "You must enter a message.")]
    [MinLength(1, ErrorMessage = "You must enter a message.")]
    [MaxLength(10000, ErrorMessage = "Message is too long.")]
    public string Message { get; set; } = null!;
}
