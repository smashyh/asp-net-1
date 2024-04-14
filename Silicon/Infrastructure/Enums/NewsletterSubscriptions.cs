namespace Infrastructure.Enums;

[Flags]
public enum NewsletterSubscriptions : byte
{
    Nothing = 0,
    DailyNewsletter = 1 << 0,
    AdvertisingUpdates = 1 << 1,
    WeekInReview = 1 << 2,
    EventUpdates = 1 << 3,
    StartupsWeekly = 1 << 4,
    Podcasts = 1 << 5,
    Everything = 0b111111,
}