using Infrastructure.Models.APIModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ContactController(HttpClient httpClient, IConfiguration config) : Controller
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IConfiguration _configuration = config;

        private async Task GetServices()
        {
            try
            {
                string apiAddress = _configuration["Api:ApiPath"] ?? "";
                string apiKey = _configuration["Api:Secret"] ?? "";

                var serviceResponse = await _httpClient.GetAsync($"{apiAddress}/Service?key={apiKey}");
                if (serviceResponse.IsSuccessStatusCode)
                {
                    var serviceResult = JsonConvert.DeserializeObject<List<string>>(await serviceResponse.Content.ReadAsStringAsync());
                    ViewData["Services"] = serviceResult;
                }
                else
                {
                    ViewData["Services"] = null;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        [HttpGet]
        [Route("/contact")]
        public async Task<IActionResult> Contact()
        {
            await GetServices();

            return View();
        }

        [HttpPost]
        [Route("/contact")]
        public async Task<IActionResult> SendContactRequest(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await GetServices();
                return View("Contact", model);
            }

            ViewData["ContactResultMessage"] = "An unexpected error occurred. Please try again later.";
            ViewData["ContactResultSuccessful"] = false;

            try
            {
                await GetServices();

                string apiAddress = _configuration["Api:ApiPath"] ?? "";
                string apiKey = _configuration["Api:Secret"] ?? "";

                var contactModel = new ContactModel
                {
                    FullName = model.FullName, 
                    Email = model.Email,
                    Service = model.Service,
                    Message = model.Message,
                };

                var content = new StringContent(JsonConvert.SerializeObject(contactModel), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{apiAddress}/Contact/SendContactRequest?key={apiKey}", content);
                Debug.WriteLine($"address = {apiAddress}/Contact/SendContactRequest?key={apiKey}");
                Debug.WriteLine(response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    ViewData["ContactResultMessage"] = "Contact request sent successfully.";
                    ViewData["ContactResultSuccessful"] = true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return View("Contact", model);
        }
    }
}
