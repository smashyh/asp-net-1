using Infrastructure.Models.APIModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
public class CourseController(HttpClient httpClient, IConfiguration config) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConfiguration _configuration = config;

    [Route("/courses")]
    public async Task<IActionResult> Courses(string category = "", string search = "", int pageNumber = 1, int pageSize = 3)
    {
        var coursesViewModel = new CourseResultViewModel();
        coursesViewModel.Category = category;
        coursesViewModel.SearchQuery = search;
        try
        {
            string apiAddress = _configuration["Api:ApiPath"] ?? "";
            string apiKey = _configuration["Api:Secret"] ?? "";

            var categoryResponse = await _httpClient.GetAsync($"{apiAddress}/Course/CourseCategories?key={apiKey}");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryResult = JsonConvert.DeserializeObject<List<string>>(await categoryResponse.Content.ReadAsStringAsync());
                coursesViewModel.Categories = categoryResult;
                if (coursesViewModel.Categories == null || !coursesViewModel.Categories.Contains(coursesViewModel.Category))
                {
                    coursesViewModel.Category = "";
                }
            }

            var response = await _httpClient.GetAsync($"{apiAddress}/Course/GetAllCourses?category={Uri.EscapeDataString(category)}&search={Uri.EscapeDataString(search)}&pageNumber={pageNumber}&pageSize={pageSize}&key=" + apiKey);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<CourseResultModel>(await response.Content.ReadAsStringAsync());
                if (result != null)
                {
                    coursesViewModel.CurrentPage = pageNumber;
                    coursesViewModel.PageSize = pageSize;
                    coursesViewModel.Courses = result.Courses;
                    coursesViewModel.TotalItemCount = result.TotalItemCount;
                    coursesViewModel.TotalPageCount = result.TotalPageCount;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return View(coursesViewModel);
    }

    [HttpGet("/courses/{courseId}")]
    public async Task<IActionResult> Course(string courseId)
    {
        try
        {
            string apiAddress = _configuration["Api:ApiPath"] ?? "";
            string apiKey = _configuration["Api:Secret"] ?? "";

            var viewModel = new CourseViewModel();

            var response = await _httpClient.GetAsync($"{apiAddress}/Course/{courseId}?key={apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var courseResult = JsonConvert.DeserializeObject<CourseModel>(await response.Content.ReadAsStringAsync());
                if (courseResult != null)
                {
                    viewModel.Course = courseResult;
                    return View(viewModel);
                }
            }
        }
        catch (Exception e) { Debug.WriteLine(e.Message); }

        return NotFound();
    }
}
