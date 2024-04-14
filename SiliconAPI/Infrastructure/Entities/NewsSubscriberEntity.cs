using Infrastructure.Enums;

namespace Infrastructure.Entities;

public class NewsSubscriberEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    // Sorry, fick lust att flexa mina bitmask-kunskaper här, hehe
    public NewsletterSubscriptions Subscriptions { get; set; }
    public DateTime DateSubscribed { get; set; } = DateTime.Now;
    public DateTime DateUpdated { get; set; } = DateTime.Now;
}
