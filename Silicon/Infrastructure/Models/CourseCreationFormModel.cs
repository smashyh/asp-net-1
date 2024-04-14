using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class CourseCreationFormModel
    {
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
        // @todo: Course thumbnail pic

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
    }
}
