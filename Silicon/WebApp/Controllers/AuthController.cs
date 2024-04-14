using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Statics;
using WebApp.Models;

namespace WebApp.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    [HttpGet]
    [Route("/signin")]
    public IActionResult SignIn(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl ?? "/";
        return View();
    }

    [HttpPost]
    [Route("/signin")]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            var signIn = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);
            if (signIn.Succeeded)
            {
                return LocalRedirect(returnUrl ?? "/");
            }
        }

        ViewData["ReturnUrl"] = returnUrl ?? "/";
        ViewData["StatusMessage"] = "The e-mail address or password is incorrect.";

        return View(viewModel);
    }

    [HttpPost]
    [Route("/signup")]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            if (!await _userManager.Users.AnyAsync(x => x.Email == viewModel.Email))
            {
                var newUser = new UserEntity
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    UserName = viewModel.Email,
                };

                string roleName = await _userManager.Users.AnyAsync() ? RoleNames.roleUser : RoleNames.roleAdmin;

                var result = await _userManager.CreateAsync(newUser, viewModel.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(newUser, roleName);

                    if (roleResult.Succeeded)
                    {
                        ViewData["SignUpSuccessful"] = true;
                        return SignUp();
                    }
                }

                ViewData["StatusMessage"] = "Something went wrong. Please try again.";
            }
            else
            {
                ViewData["StatusMessage"] = "A user with provided e-mail address already exists.";
            }
        }

        return View(viewModel);
    }

    [HttpGet]
    [Route("/signup")]
    public IActionResult SignUp()
    {
        return View();
    }

    [Route("/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return LocalRedirect("/");
    }
}
