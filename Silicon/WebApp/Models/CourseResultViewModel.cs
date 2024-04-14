using Infrastructure.Models;
using Infrastructure.Models.APIModels;

namespace WebApp.Models;

public class CourseResultViewModel
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItemCount { get; set; }
    public int TotalPageCount { get; set; }
    public string? Category { get; set; }
    public string? SearchQuery { get; set; }
    public IEnumerable<CourseModel>? Courses { get; set; }
    public IEnumerable<string>? Categories { get; set; }
}
