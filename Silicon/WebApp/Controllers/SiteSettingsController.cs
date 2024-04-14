using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class SiteSettingsController : Controller
{
    public IActionResult Theme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddYears(1),
        };
        Response.Cookies.Append("theme", mode, option);

        return Ok();
    }

    public IActionResult AcceptCookies()
    {
        var option = new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddYears(1),
            HttpOnly = true,
            Secure = true,
        }; 
        Response.Cookies.Append("accept_cookies", "true", option);

        return Ok();
    }
}
