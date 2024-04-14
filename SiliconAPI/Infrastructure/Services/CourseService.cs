using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Models.CreationForms;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Services
{
    public class CourseService(CourseRepository courseRepository, CourseCreatorRepository courseCreatorRepository, CourseCategoryRepository courseCategoryRepository, CourseBadgeRepository courseBadgeRepository)
    {
        private readonly CourseRepository _courseRepository = courseRepository;
        private readonly CourseBadgeRepository _courseBadgeRepository = courseBadgeRepository;
        private readonly CourseCategoryRepository _courseCategoryRepository = courseCategoryRepository;
        private readonly CourseCreatorRepository _courseCreatorRepository = courseCreatorRepository;

        public async Task<bool> CreateCourseAsync(CourseCreationFormModel form)
        {
            try
            {
                var newCourse = new CourseEntity();
                newCourse.CourseName = form.CourseName;
                newCourse.CourseShortDescription = form.CourseShortDescription;

                newCourse.CourseBadges = new List<CourseBadgeEntity>();
                // @todo: Make a factory or let a service take care of this.
                foreach (var badge in form.CourseBadges)
                {
                    // Check if there are any badges matching the models
                    // in the database already and use those.
                    var badgeEntity = await _courseBadgeRepository.GetAsync(x =>
                        x.BadgeLabel == badge.BadgeLabel
                        && x.BackgroundColorStyling == badge.BackgroundColorStyling
                        && x.ColorStyling == badge.ColorStyling
                        && x.Important == badge.Important);

                    // In the event that it doesn't, create a new one, add
                    // to the database.
                    badgeEntity ??= await _courseBadgeRepository.CreateAsync(new CourseBadgeEntity 
                    {
                        BadgeLabel = badge.BadgeLabel,
                        BackgroundColorStyling = badge.BackgroundColorStyling,
                        ColorStyling = badge.ColorStyling,
                        Important = badge.Important,
                    });

                    if (badgeEntity == null)
                        throw new NullReferenceException("Course creation failed: Creation of badge entity failed.");

                    newCourse.CourseBadges.Add(badgeEntity);
                }

                newCourse.AverageRating = form.AverageRating;
                newCourse.ReviewCount = form.ReviewCount;
                newCourse.LikeCount = form.LikeCount;
                newCourse.CourseLengthHours = form.CourseLengthHours;

                // @todo: Make a factory or let a service take care of this.

                var creatorEntity = await _courseCreatorRepository.GetAsync(x =>
                    x.FirstName == form.CourseCreator.FirstName
                    && x.LastName == form.CourseCreator.LastName
                    && x.Description == form.CourseCreator.Description
                    && x.YouTubeSubscriberCount == form.CourseCreator.YouTubeSubscriberCount
                    && x.FacebookFollowerCount == form.CourseCreator.FacebookFollowerCount
                    && x.ProfileImageUrl == form.CourseCreator.ProfileImageUrl);

                creatorEntity ??= await _courseCreatorRepository.CreateAsync(new CourseCreatorEntity
                {
                    FirstName = form.CourseCreator.FirstName,
                    LastName = form.CourseCreator.LastName,
                    Description = form.CourseCreator.Description,
                    YouTubeSubscriberCount = form.CourseCreator.YouTubeSubscriberCount,
                    FacebookFollowerCount = form.CourseCreator.FacebookFollowerCount,
                    ProfileImageUrl = form.CourseCreator.ProfileImageUrl,
                });

                if (creatorEntity == null)
                    throw new NullReferenceException("Course creation failed: Creation of course creator entity failed.");

                newCourse.CourseCreatorId = creatorEntity.Id;
                newCourse.CourseCreator = creatorEntity;

                newCourse.CourseDescription = form.CourseDescription;
                newCourse.WhatYoullLearn = form.WhatYoullLearn;
                newCourse.ProgramDetails = form.ProgramDetails;
                newCourse.OnDemandVideoHourCount = form.OnDemandVideoHourCount;
                newCourse.ArticleCount = form.ArticleCount;
                newCourse.DownloadableResourceCount = form.DownloadableResourceCount;
                newCourse.HasFullLifetimeAccess = form.HasFullLifetimeAccess;
                newCourse.HasCertificateOfCompletion = form.HasCertificateOfCompletion;
                newCourse.Price = form.Price;
                newCourse.DiscountPrice = form.DiscountPrice;

                // @todo: Make a factory or let a service take care of this.

                var categoryEntity = await _courseCategoryRepository.GetAsync(x => x.CategoryName == form.Category);
                categoryEntity ??= await _courseCategoryRepository.CreateAsync(new CourseCategoryEntity() { CategoryName = form.Category });
                
                if (creatorEntity == null)
                    throw new NullReferenceException("Course creation failed: Creation of course category entity failed.");
                
                newCourse.CategoryId = categoryEntity.Id;
                newCourse.Category = categoryEntity;

                newCourse.CourseThumbnailImageUrl = form.CourseThumbnailImageUrl;

                newCourse.LastUpdated = DateTime.Now;

                var result = await _courseRepository.CreateAsync(newCourse);
                if (result != null)
                    return true;

                Debug.WriteLine("Course creation failed: CreateAsync returned null.");
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false;
        }

        public async Task<CourseEntity?> GetCourseAsync(Expression<Func<CourseEntity, bool>> expression, bool getRelations)
        {
            try
            {
                return await _courseRepository.GetAsync(expression, getRelations);
            }
            catch(Exception ex) { Debug.WriteLine(ex.Message); }

            return null;
        }

        public async Task<IEnumerable<CourseEntity>> GetCoursesAsync(Expression<Func<CourseEntity, bool>> expression, bool getRelations)
        {
            try
            {
                return await _courseRepository.GetAllAsync(expression, getRelations) ?? new List<CourseEntity>();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return new List<CourseEntity>();
        }

        public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync(bool getRelations)
        {
            try
            {
                return await _courseRepository.GetAllAsync(getRelations) ?? new List<CourseEntity>();
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return new List<CourseEntity>();
        }

        public async Task<CourseResultModel?> GetAllCourseModelsAsync(CourseSearchModel search)
        {
            try
            {
                var query = _courseRepository.GetSet(true);

                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.CategoryName))
                    {
                        query = query.Where(x => x.Category.CategoryName == search.CategoryName);
                    }

                    if (!string.IsNullOrEmpty(search.SearchString))
                    {
                        query = query.Where(x
                            => x.CourseName.Contains(search.SearchString)
                            || (x.CourseCreator.FirstName + " " + x.CourseCreator.LastName).Contains(search.SearchString));
                    }

                    query = query.OrderByDescending(x => x.LastUpdated);

                    var courseResult = new CourseResultModel();

                    int totalItemCount = await query.CountAsync();
                    if (totalItemCount > 0)
                    {
                        int totalPageCount = (int)Math.Ceiling(totalItemCount / (decimal)search.PageSize);

                        query = query.Skip((search.PageNumber - 1) * search.PageSize).Take(search.PageSize);

                        IEnumerable<CourseEntity> list = await query.ToListAsync();

                        var courses = new List<CourseModel>();
                        foreach (var item in list)
                        {
                            courses.Add(CourseModelFactory.Create(item) ?? throw new NullReferenceException());
                        }

                        courseResult.TotalItemCount = totalItemCount;
                        courseResult.TotalPageCount = totalPageCount;
                        courseResult.Courses = courses;
                    }

                    return courseResult;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return null;
        }

        public async Task<bool> UpdateCourseAsync(CourseEntity entity)
        {
            try
            {
                if (entity == null)
                    return false;

                entity.LastUpdated = DateTime.Now;

                var updated = await _courseRepository.UpdateAsync(entity);
                return updated != null;
            } 
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }

        public async Task<bool> DeleteCourseAsync(CourseEntity entity)
        {
            try
            {
                if (entity == null)
                    return false;

                bool deleted = await _courseRepository.DeleteAsync(entity);
                return deleted;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }
    }
}
