using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(AccountService accountService, AddressService addressService, SignInManager<UserEntity> signInManager) : Controller
{
    //private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AccountService _accountService = accountService;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AddressService _addressService = addressService;

    [Route("/account/{menuType}")]
    [HttpGet]
    public IActionResult AccountSettings(string menuType)
    {
        // @todo: Prevent being able to navigate to "menus" that don't exist

        ViewData["AccountSettingsMenu"] = menuType;
        return View();
    }

    #region Details

    [HttpPut, HttpPost]
    public async Task<IActionResult> UpdateBasicInfo(AccountDetailsBasicInfoViewModel model)
    {
        if (ModelState.IsValid)
        {
            TempData["BasicInfoStatusMessage"] = "Something went wrong. Please try again.";
            TempData["BasicInfoSuccess"] = false;

            try
            {
                var infoModel = new AccountBasicInfoModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Biography = model.Biography,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await _accountService.UpdateBasicInfoAsync(User, infoModel);
                if (result != null && result.Succeeded)
                {
                    TempData["BasicInfoStatusMessage"] = "Info has been successfully updated.";
                    TempData["BasicInfoSuccess"] = true;
                }
                //var userEntity = await _userManager.GetUserAsync(User);
                //if (userEntity != null)
                //{
                //    userEntity.FirstName = model.FirstName;
                //    userEntity.LastName = model.LastName;
                //    userEntity.Email = model.Email; // @todo: send confirmation if changed?
                //    userEntity.PhoneNumber = model.PhoneNumber;
                //    userEntity.Biography = model.Biography;

                //    var result = await _userManager.UpdateAsync(userEntity);

                //    if (result.Succeeded)
                //    {
                //        TempData["BasicInfoStatusMessage"] = "Info has been successfully updated.";
                //        TempData["BasicInfoSuccess"] = true;
                //    }
                //}
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        return LocalRedirect("/account/details");
    }

    [HttpPut, HttpPost]
    public async Task<IActionResult> UpdateAddress(AccountDetailsAddressViewModel model)
    {
        if (ModelState.IsValid)
        {
            TempData["AddressStatusMessage"] = "Something went wrong. Please try again.";
            TempData["AddressSuccess"] = false;

            try
            {
                bool success = false;

                var userEntity = await _accountService.GetUserAsync(User);
                if (userEntity != null)
                {
                    var address = await _addressService.GetAddressAsync(x => x.UserId == userEntity.Id);
                    if (address != null)
                    {
                        address.Address_1 = model.Address_1;
                        address.Address_2 = model.Address_2;
                        address.PostalCode = model.PostalCode;
                        address.City = model.City;

                        success = await _addressService.UpdateAddressAsync(address);
                    }
                    else
                    {
                        address = AddressFactory.Create(userEntity, model.Address_1, model.Address_2, model.PostalCode, model.City);

                        success = address != null && await _addressService.CreateAsync(address);
                    }
                }

                if (success)
                {
                    TempData["AddressStatusMessage"] = "Address successfully updated.";
                    TempData["AddressSuccess"] = true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        return LocalRedirect("/account/details");
    }

    #endregion

    #region Security

    [HttpPost]
    public async Task<IActionResult> UpdatePassword(AccountSecurityChangePasswordViewModel model)
    {
        IActionResult Default() => LocalRedirect("/account/security");

        if (ModelState.IsValid)
        {
            TempData["PasswordStatusMessage"] = "Something went wrong. Please try again.";
            TempData["PasswordSuccess"] = false;

            var result = await _accountService.UpdatePasswordAsync(User, model.CurrentPassword, model.NewPassword);
            if (result != null && result.Succeeded)
            {
                TempData["PasswordStatusMessage"] = "Password successfully changed.";
                TempData["PasswordSuccess"] = true;
            }
            else
            {
                TempData["PasswordStatusMessage"] = "The current password is incorrect. Please try again.";
            }

            return Default();
        }

        return Default();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAccount(AccountSecurityDeleteAccountViewModel model)
    {
        IActionResult Default() => LocalRedirect("/account/security");

        if (ModelState.IsValid)
        {
            TempData["DeleteAccountStatusMessage"] = "Something went wrong. Please try again.";
            TempData["DeleteAccountSuccess"] = false;

            bool result = await _accountService.DeleteUserAsync(User);
            if (result)
            {
                TempData.Remove("DeleteAccountStatusMessage");
                TempData.Remove("DeleteAccountSuccess"); 
                return LocalRedirect("/");
            }
        }

        return Default();
    }

    #endregion

    [HttpPost]
    public async Task<IActionResult> UploadProfileImage(IFormFile file)
    {
        await _accountService.UploadProfileImageAsync(User, file);
        return LocalRedirect("/account/details");
    }
}
