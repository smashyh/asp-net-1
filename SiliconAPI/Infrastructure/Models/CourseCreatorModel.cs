using Infrastructure.Entities;

namespace Infrastructure.Models
{
    public class CourseCreatorModel
    {
        public CourseCreatorModel() { }
        public CourseCreatorModel(string firstName, string lastName, string? description, int youTubeSubscriberCount, int facebookFollowerCount, string? profileImageUrl)
        {
            FirstName = firstName;
            LastName = lastName;
            Description = description;
            YouTubeSubscriberCount = youTubeSubscriberCount;
            FacebookFollowerCount = facebookFollowerCount;
            ProfileImageUrl = profileImageUrl;
        }

        public CourseCreatorModel(CourseCreatorEntity courseCreatorEntity) 
        {
            FirstName = courseCreatorEntity.FirstName;
            LastName = courseCreatorEntity.LastName;
            Description = courseCreatorEntity.Description;
            YouTubeSubscriberCount = courseCreatorEntity.YouTubeSubscriberCount;
            FacebookFollowerCount = courseCreatorEntity.FacebookFollowerCount;
            ProfileImageUrl = courseCreatorEntity.ProfileImageUrl;
        }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public int? YouTubeSubscriberCount { get; set; }
        public int? FacebookFollowerCount { get; set; }
        public string? ProfileImageUrl { get; set; } = null!;
    }
}
