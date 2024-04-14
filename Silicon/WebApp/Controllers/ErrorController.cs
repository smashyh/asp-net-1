using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/StatusCode/{errorCode}")]
        public IActionResult HandleError(int errorCode)
        {
            // todo: more error pages?
            return LocalRedirect("/not-found");
        }

        [Route("not-found")]
        public IActionResult NotFoundError()
        {
            return View();
        }
    }
}
