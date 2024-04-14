using Infrastructure.Models;

namespace Infrastructure.Models.APIModels;

public class CourseModel
{
    public Guid Id { get; set; }
    public string CourseName { get; set; } = null!;
    public string CourseShortDescription { get; set; } = null!;
    public IEnumerable<CourseBadgeModel> CourseBadges { get; set; } = null!;
    public decimal AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int LikeCount { get; set; }
    public int CourseLengthHours { get; set; }
    public CourseCreatorModel CourseCreator { get; set; } = null!;
    public string CourseDescription { get; set; } = null!;
    public string WhatYoullLearn { get; set; } = null!;
    public string ProgramDetails { get; set; } = null!;
    public int? OnDemandVideoHourCount { get; set; }
    public int? ArticleCount { get; set; }
    public int? DownloadableResourceCount { get; set; }
    public bool? HasFullLifetimeAccess { get; set; }
    public bool? HasCertificateOfCompletion { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public string Category { get; set; } = null!;
    public string? CourseThumbnailImageUrl { get; set; } = null!;
    public DateTime LastUpdated { get; set; }
}
