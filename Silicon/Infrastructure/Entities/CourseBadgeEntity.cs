namespace Infrastructure.Entities
{
    /// <summary>
    /// Badges like "Best Seller", "Digital", etc.
    /// </summary>
    public class CourseBadgeEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BadgeLabel { get; set; } = null!;
        public string BackgroundColorStyling { get; set; } = null!;
        public string ColorStyling { get; set; } = null!;
        public bool Important { get; set; }
    }
}
