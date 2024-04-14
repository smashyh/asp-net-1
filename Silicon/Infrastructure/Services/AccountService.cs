using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

namespace Infrastructure.Services;

public class AccountService(UserManager<UserEntity> userManager, DataContext dataContext, IConfiguration config, AddressService addressService, SignInManager<UserEntity> signInManager)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AddressService _addressService = addressService;
    private readonly DataContext _dataContext = dataContext;
    private readonly IConfiguration _config = config;

    public async Task<UserEntity?> GetUserAsync(ClaimsPrincipal userClaims)
    {
        try
        {
            if (userClaims != null)
            {
                return await _userManager.GetUserAsync(userClaims);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }


        return null;
    }

    public async Task<IdentityResult?> UpdateBasicInfoAsync(ClaimsPrincipal userClaims, AccountBasicInfoModel model)
    {
        var userEntity = await _userManager.GetUserAsync(userClaims);
        if (userEntity != null)
        {
            userEntity.FirstName = model.FirstName;
            userEntity.LastName = model.LastName;
            userEntity.Email = model.Email; // @todo: send confirmation if changed?
            userEntity.PhoneNumber = model.PhoneNumber;
            userEntity.Biography = model.Biography;

            var result = await _userManager.UpdateAsync(userEntity);
            return result;
        }

        return null;
    }

    public async Task<IdentityResult?> UpdatePasswordAsync(ClaimsPrincipal userClaims, string password, string newPassword)
    {
        try
        {
            var user = await _userManager.GetUserAsync(userClaims);
            if (user != null)
            {
                bool checkPasswordResult = await _userManager.CheckPasswordAsync(user, password);
                if (checkPasswordResult)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, password, newPassword);
                    return changePasswordResult;
                }
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null;
    }

    public async Task<bool> DeleteUserAsync(ClaimsPrincipal userClaims)
    {
        try
        {
            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var addresses = await _addressService.GetAllAddressesAsync(x => x.UserId == user.Id);
                foreach (var address in addresses)
                {
                    await _addressService.DeleteAddressAsync(address);
                }

                await _signInManager.SignOutAsync();
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    public async Task<bool> UploadProfileImageAsync(ClaimsPrincipal userClaims, IFormFile file)
    {
        try
        {
            if (userClaims == null || file == null || file.Length == 0)
            {
                return false;
            }

            var user = await _userManager.GetUserAsync(userClaims);
            if (user == null)
                return false;

            string fileName = $"pr_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), _config["FilePath:ProfileUploadPath"]!, fileName);

            using var fs = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs);

            user.ProfileImage = fileName;

            _dataContext.Update(user);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<string> GetProfileImgSrc(ClaimsPrincipal userClaims)
    {
        try
        {
            var user = await _userManager.GetUserAsync(userClaims);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.ProfileImage))
                {
                    return $"{_config["FilePath:ProfileImagePath"]}/{user.ProfileImage}";
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return _config["FilePath:ProfileImageDefaultSrc"]!;
    }
}
