using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.CreationForms
{
    public class CourseCreationFormModel
    {
        public CourseCreationFormModel() { }

        public CourseCreationFormModel(string courseName, string courseShortDescription,
            IEnumerable<CourseBadgeModel> courseBadges, decimal averageRating, int reviewCount,
            int likeCount, int courseLengthHours, CourseCreatorModel courseCreator,
            string courseDescription, string whatYoullLearn, string programDetails,
            int? onDemandVideoHourCount, int? articleCount, int? downloadableResourceCount,
            bool? hasFullLifetimeAccess, bool? hasCertificateOfCompletion, decimal price,
            decimal? discountPrice, string category, string? courseThumbnailImageUrl)
        {
            CourseName = courseName;
            CourseShortDescription = courseShortDescription;
            ArgumentNullException.ThrowIfNull(courseBadges);
            CourseBadges = courseBadges;
            AverageRating = averageRating;
            ReviewCount = reviewCount;
            LikeCount = likeCount;
            CourseLengthHours = courseLengthHours;
            ArgumentNullException.ThrowIfNull(courseCreator);
            CourseCreator = courseCreator;
            CourseDescription = courseDescription;
            WhatYoullLearn = whatYoullLearn;
            ProgramDetails = programDetails;
            OnDemandVideoHourCount = onDemandVideoHourCount;
            ArticleCount = articleCount;
            DownloadableResourceCount = downloadableResourceCount;
            HasFullLifetimeAccess = hasFullLifetimeAccess;
            HasCertificateOfCompletion = hasCertificateOfCompletion;
            Price = price;
            DiscountPrice = discountPrice;
            Category = category;
            CourseThumbnailImageUrl = courseThumbnailImageUrl;
        }

        public string CourseName { get; set; } = null!;
        public string CourseShortDescription { get; set; } = null!;
        public IEnumerable<CourseBadgeModel> CourseBadges { get; set; } = null!;
        public decimal AverageRating { get; set; }
        // @todo: RatingEntities?
        public int ReviewCount { get; set; }
        public int LikeCount { get; set; }
        // @todo: LikeEntities?
        public int CourseLengthHours { get; set; }
        public CourseCreatorModel CourseCreator { get; set; } = null!;

        // Course details
        public string CourseDescription { get; set; } = null!;
        public string WhatYoullLearn { get; set; } = null!; // Each bullet point is divided with a '|'.
        public string ProgramDetails { get; set; } = null!; // Each step is divided with a '|'.

        // "This course includes:"
        public int? OnDemandVideoHourCount { get; set; }
        public int? ArticleCount { get; set; }
        public int? DownloadableResourceCount { get; set; }
        public bool? HasFullLifetimeAccess { get; set; }
        public bool? HasCertificateOfCompletion { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Category { get; set; } = null!;

        public string? CourseThumbnailImageUrl { get; set; }
    }
}
