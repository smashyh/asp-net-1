using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AccountSecurityDeleteAccountViewModel
{
    [Display(Name = "Yes, I want to delete my account.")]
    [Range(typeof(bool), "true", "true", ErrorMessage = "Please agree to deleting your account first.")]
    public bool DeleteAccountConfirm { get; set; }
}
