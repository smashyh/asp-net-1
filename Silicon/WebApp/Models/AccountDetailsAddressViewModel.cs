using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AccountDetailsAddressViewModel
{
    [Display(Name = "Address line 1", Prompt = "Enter an address.")]
    [Required(ErrorMessage = "A valid address is required.")]
    [MinLength(2, ErrorMessage = "Address must be at least 2 characters.")]
    [DataType(DataType.Text)]
    public string Address_1 { get; set; } = null!;

    [Display(Name = "Address line 2", Prompt = "Enter a secondary address (optional).")]
    [DataType(DataType.Text)]
    public string? Address_2 { get; set; }

    //[RegularExpression("^[0-9]", ErrorMessage = "This field can only contain numbers.")]
    [MinLength(2, ErrorMessage = "Postal code must be at least 2 characters.")]
    [Required(ErrorMessage = "You must provide a postal code.")]
    [DataType(DataType.Text)]
    public string PostalCode { get; set; } = null!;

    [MinLength(1, ErrorMessage = "City must be at least 1 character.")]
    [Required(ErrorMessage = "You must provide a city.")]
    [DataType(DataType.Text)]
    public string City { get; set; } = null!;
}
