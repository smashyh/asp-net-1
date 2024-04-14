using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class CourseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        // Everything belonging to the header of the course page.
        public string CourseName { get; set; } = null!;
        public string CourseShortDescription { get; set; } = null!;
        public ICollection<CourseBadgeEntity> CourseBadges { get; set; } = null!;
        [Column(TypeName = "decimal(5,2)")]
        public decimal AverageRating { get; set; }
        // @todo: RatingEntities?
        public int ReviewCount { get; set; }
        public int LikeCount { get; set; }
        // @todo: LikeEntities?
        public int CourseLengthHours { get; set; }
        public Guid CourseCreatorId { get; set; }
        public CourseCreatorEntity CourseCreator { get; set; } = null!;
        // @todo: Course thumbnail pic

        // Course details
        public string CourseDescription { get; set; } = null!;
        public string WhatYoullLearn { get; set; } = null!; // Each bullet point is divided with a '|'.
        public string ProgramDetails { get; set; } = null!; // Each step is divided with a '|'.

        // "This course includes:"
        public int? OnDemandVideoHourCount { get; set; }
        public int? ArticleCount { get; set; }
        public int? DownloadableResourceCount {  get; set; }
        public bool? HasFullLifetimeAccess { get; set; }
        public bool? HasCertificateOfCompletion { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Column(TypeName = "money")]
        public decimal? DiscountPrice { get; set; }

        public Guid CategoryId { get; set; }
        public CourseCategoryEntity Category { get; set; } = null!;

        public string? CourseThumbnailImageUrl { get; set; } = null!;

        public DateTime LastUpdated { get; set; }
    }
}
