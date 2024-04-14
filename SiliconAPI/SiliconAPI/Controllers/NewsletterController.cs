using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SiliconAPI.Attributes;
using System.Diagnostics;
using System.Net;

namespace SiliconAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class NewsletterController(NewsSubscriberService newsSubscriberService) : ControllerBase
{
    private readonly NewsSubscriberService _newsSubscriberService = newsSubscriberService;

    [HttpPost("Subscribe")]
    public async Task<IActionResult> Subscribe(NewsSubscriberModel model)
    {
        try
        {
            HttpStatusCode result = await _newsSubscriberService.CreateAsync(model);

            return StatusCode((int)result);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("Unsubscribe/{email}")]
    public async Task<IActionResult> Unsubscribe(string email)
    {
        try
        {
            var entity = await _newsSubscriberService.GetAsync(x => x.Email == email);
            if (entity == null)
            {
                return NotFound();
            }

            bool result = await _newsSubscriberService.DeleteAsync(entity);
            if (result)
            {
                return Ok();
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}
