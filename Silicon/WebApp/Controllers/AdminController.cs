using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Statics;

namespace WebApp.Controllers;

[Authorize(Roles = RoleNames.roleAdmin)]
public class AdminController : Controller
{
    [Route("/admin/control-panel")]
    public IActionResult ControlPanel()
    {
        return View();
    }
}
