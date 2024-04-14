using Infrastructure.Entities;

namespace Infrastructure.Models;

public class CourseCreatorModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public int? YouTubeSubscriberCount { get; set; }
    public int? FacebookFollowerCount { get; set; }
    public string? ProfileImageUrl { get; set; }
}
