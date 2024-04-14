using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
    {
        public DbSet<CourseEntity> CourseEntities { get; set; }
        public DbSet<CourseCategoryEntity> CourseCategories { get; set; }
        public DbSet<CourseCreatorEntity> CourseCreatorEntities { get; set; }
        public DbSet<CourseBadgeEntity> CourseBadgeEntities { get; set; }
        public DbSet<ServiceEntity> ServiceEntities { get; set; }
        public DbSet<ContactEntity> ContactEntities { get; set; }
        public DbSet<NewsSubscriberEntity> NewsSubscriberEntities { get; set; }
    }
}
