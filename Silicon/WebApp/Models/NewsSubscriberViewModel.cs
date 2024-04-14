using System.ComponentModel.DataAnnotations;
using WebApp.Statics;

namespace WebApp.Models;

public class NewsSubscriberViewModel
{
    [Display(Name = "E-mail", Prompt = "Your E-mail.")]
    [RegularExpression(RegExStrings.EmailRegEx, ErrorMessage = "A valid e-mail is required.")]
    [Required(ErrorMessage = "You must enter an e-mail address.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Display(Name = "Daily Newsletter")]
    public bool DailyNewsletter { get; set; }

    [Display(Name = "Advertising Updates")]
    public bool AdvertisingUpdates { get; set; }

    [Display(Name = "Week in Review")]
    public bool WeekInReview { get; set; }

    [Display(Name = "Event Updates")]
    public bool EventUpdates { get; set; }

    [Display(Name = "Startups Weekly")]
    public bool StartupsWeekly { get; set; }

    [Display(Name = "Podcasts")]
    public bool Podcasts { get; set; }
}
