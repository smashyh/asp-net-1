using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SiliconAPI.Attributes;
using System.Diagnostics;

namespace SiliconAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class ServiceController(ServiceService serviceService) : ControllerBase
{
    private readonly ServiceService _serviceService = serviceService;

    [HttpPost("AddService/{serviceName}")]
    public async Task<IActionResult> AddService(string serviceName)
    {
        try
        {
            if (await _serviceService.ExistsAsync(serviceName))
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            var result = await _serviceService.AddServiceAsync(new ServiceEntity
            {
                ServiceName = serviceName,
            });

            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServices()
    {
        try
        {
            var services = await _serviceService.GetAllServicesAsync();
            if (services == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var result = new List<string>();
            foreach (var service in services)
            {
                result.Add(service.ServiceName);
            }

            return Ok(result);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}
