using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace SiliconAPI.Configurations;

public static class Registrations
{
    public static void RegisterDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApiContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")));
    }

    public static void RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CourseBadgeRepository>();
        services.AddScoped<CourseCategoryRepository>();
        services.AddScoped<CourseCreatorRepository>();
        services.AddScoped<CourseRepository>();
        services.AddScoped<ServiceRepository>();
        services.AddScoped<ContactRepository>();
        services.AddScoped<NewsSubscriberRepository>();
    }

    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CourseService>();
        services.AddScoped<ServiceService>();
        services.AddScoped<ContactService>();
        services.AddScoped<NewsSubscriberService>();
    }
}
