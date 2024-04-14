using Infrastructure.Entities;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Factories
{
    public static class CourseModelFactory
    {
        public static CourseModel? Create(CourseEntity course)
        {
            try
            {
                CourseModel courseMdl = new CourseModel();
                courseMdl.Id = course.Id;
                courseMdl.CourseName = course.CourseName;
                courseMdl.CourseShortDescription = course.CourseShortDescription;
                courseMdl.CourseBadges = CourseBadgeFactory.Create(course.CourseBadges);
                courseMdl.AverageRating = course.AverageRating;
                courseMdl.ReviewCount = course.ReviewCount;
                courseMdl.LikeCount = course.LikeCount;
                courseMdl.CourseLengthHours = course.CourseLengthHours;
                courseMdl.CourseCreator = CourseCreatorFactory.Create(course.CourseCreator);
                courseMdl.CourseDescription = course.CourseDescription;
                courseMdl.WhatYoullLearn = course.WhatYoullLearn;
                courseMdl.ProgramDetails = course.ProgramDetails;
                courseMdl.OnDemandVideoHourCount = course.OnDemandVideoHourCount;
                courseMdl.ArticleCount = course.ArticleCount;
                courseMdl.DownloadableResourceCount = course.DownloadableResourceCount;
                courseMdl.HasFullLifetimeAccess = course.HasFullLifetimeAccess;
                courseMdl.HasCertificateOfCompletion = course.HasCertificateOfCompletion;
                courseMdl.Price = course.Price;
                courseMdl.DiscountPrice = course.DiscountPrice;
                courseMdl.Category = course.Category.CategoryName;
                courseMdl.CourseThumbnailImageUrl = course.CourseThumbnailImageUrl;
                courseMdl.LastUpdated = course.LastUpdated;
                return courseMdl;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null;
        }

        public static IEnumerable<CourseModel>? Create(IEnumerable<CourseEntity> courseEntities)
        {
            try
            {
                var list = new List<CourseModel>();
                foreach (var courseEntity in courseEntities)
                {
                    list.Add(Create(courseEntity) ?? throw new NullReferenceException());
                }

                return list;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null;
        }
    }
}
