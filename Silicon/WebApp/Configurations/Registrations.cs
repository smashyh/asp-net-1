using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Helpers;
using WebApp.Middleware;

namespace WebApp.Configurations;

public static class Registrations
{
    public static void RegisterDbContexts(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
    }

    public static void RegisterRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<AddressRepository>();
    }

    public static void RegisterServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<AddressService>();
        builder.Services.AddScoped<AccountService>();
    }

    public static void RegisterMisc(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ApiCommunicator>();
    }

    public static void RegisterMiddleware(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<AuthMiddleware>();
        builder.Services.AddScoped<RoleMiddleware>();
    }
}
