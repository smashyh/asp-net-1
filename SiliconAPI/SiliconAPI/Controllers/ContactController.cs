using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiliconAPI.Attributes;
using System.Diagnostics;
using System.Net;

namespace SiliconAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class ContactController(ContactService contactService) : ControllerBase
    {
        private readonly ContactService _contactService = contactService;

        [HttpPost("SendContactRequest")]
        public async Task<IActionResult> SendContactRequest(ContactModel model)
        {
            try
            {
                HttpStatusCode result = await _contactService.AddContact(model);

                return StatusCode((int)result);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
