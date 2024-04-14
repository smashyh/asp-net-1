
using Microsoft.AspNetCore.Identity;
using WebApp.Statics;

namespace WebApp.Middleware
{
    /// <summary>
    /// Middleware to make sure all roles exist.
    /// </summary>
    public class RoleMiddleware(RoleManager<IdentityRole> roleManager) : IMiddleware
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            foreach (var role in RoleNames.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            await next(context);
        }
    }
}
