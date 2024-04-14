using Infrastructure.Enums;

namespace Infrastructure.Models;

public class NewsSubscriberModel
{
    public string Email { get; set; } = null!;
    public NewsletterSubscriptions Subscriptions { get; set; }

    //public bool DailyNewsletter { get; set; }
    //public bool AdvertisingUpdates { get; set; }
    //public bool WeekInReview { get; set; }
    //public bool EventUpdates { get; set; }
    //public bool StartupsWeekly { get; set; }
    //public bool Podcasts { get; set; }
}
