using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Models;
using Infrastructure.Models.CreationForms;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using SiliconAPI.Attributes;
using System.Diagnostics;

namespace SiliconAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class CourseController(CourseService courseService, CourseBadgeRepository badgeRepository, CourseCategoryRepository categoryRepository, CourseCreatorRepository creatorRepository) : ControllerBase
{
    private readonly CourseService _courseService = courseService;
    private readonly CourseBadgeRepository _badgeRepository = badgeRepository;
    private readonly CourseCategoryRepository _categoryRepository = categoryRepository;
    private readonly CourseCreatorRepository _creatorRepository = creatorRepository;

    [HttpPost("AddCourseCreator")]
    public async Task<IActionResult> AddCourseCreator(CourseCreatorModel model)
    {
        try
        {
            var result = await _creatorRepository.CreateAsync(new CourseCreatorEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Description = model.Description,
                YouTubeSubscriberCount = model.YouTubeSubscriberCount,
                FacebookFollowerCount = model.FacebookFollowerCount,
                ProfileImageUrl = model.ProfileImageUrl,
            });

            if (result != null)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("AddCourseCategory/{categoryName}")]
    public async Task<IActionResult> AddCourseCategory(string categoryName)
    {
        try
        {
            if (await _categoryRepository.GetAsync(x => x.CategoryName == categoryName) != null)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            var result = await _categoryRepository.CreateAsync(new CourseCategoryEntity
            {
                CategoryName = categoryName,
            });

            if (result != null)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("AddCourseBadge")]
    public async Task<IActionResult> AddCourseBadge(CourseBadgeModel form)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var result = await _badgeRepository.CreateAsync(new CourseBadgeEntity
            {
                BadgeLabel = form.BadgeLabel,
                BackgroundColorStyling = form.BackgroundColorStyling,
                ColorStyling = form.ColorStyling,
                Important = form.Important,
            });

            if (result != null)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("AddCourse")]
    public async Task<IActionResult> AddCourse(CourseCreationFormModel form)
    {
        try
        {
            bool result = await _courseService.CreateCourseAsync(form);
            if (result)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet("CourseBadges")]
    public async Task<IActionResult> GetCourseBadges()
    {
        try
        {
            var badges = await _badgeRepository.GetAllAsync();

            var badgeModels = new List<CourseBadgeModel>();
            foreach (var badge in badges)
            {
                badgeModels.Add(CourseBadgeFactory.Create(badge));
            }

            return Ok(badgeModels);
        }
        catch (Exception e) { Debug.WriteLine(e.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet("CourseCreators")]
    public async Task<IActionResult> GetCourseCreators()
    {
        try
        {
            var entities = await _creatorRepository.GetAllAsync();

            var models = new List<CourseCreatorModel>();
            foreach (var entity in entities)
            {
                models.Add(CourseCreatorFactory.Create(entity));
            }

            return Ok(models);
        }
        catch (Exception e) { Debug.WriteLine(e.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet("CourseCategories")]
    public async Task<IActionResult> GetCourseCategories()
    {
        try
        {
            var entities = await _categoryRepository.GetAllAsync();

            var models = new List<string>();
            foreach (var entity in entities)
            {
                models.Add(entity.CategoryName);
            }

            return Ok(models);
        }
        catch (Exception e) { Debug.WriteLine(e.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetCourse(string courseId = "")
    {
        try
        {
            var courseEntity = await _courseService.GetCourseAsync(x => x.Id.ToString() == courseId, true);
            if (courseEntity != null)
            {
                var query = CourseModelFactory.Create(courseEntity);

                if (query != null)
                {
                    return Ok(query);
                }
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet("GetAllCourses")]
    public async Task<IActionResult> GetAllCourses(string category = "", string search = "", int pageNumber = 1, int pageSize = 9)
    {
        CourseSearchModel searchMdl = new CourseSearchModel
        {
            CategoryName = category,
            SearchString = search,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var query = await _courseService.GetAllCourseModelsAsync(searchMdl);

        return query != null ? Ok(query) : StatusCode(500);
    }
}
