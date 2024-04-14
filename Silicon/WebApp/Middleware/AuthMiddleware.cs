
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Middleware
{
    /// <summary>
    /// Middleware to make sure ghost accounts aren't signed in.
    /// </summary>
    public class AuthMiddleware(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager) : IMiddleware
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _userManager.GetUserAsync(context.User) == null)
            {
                await _signInManager.SignOutAsync();
                //_signInManager.SignOutAsync().Wait();
            }

            await next(context);
        }
    }
}
