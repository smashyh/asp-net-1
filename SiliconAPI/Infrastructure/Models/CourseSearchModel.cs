namespace Infrastructure.Models;

public class CourseSearchModel
{
    public string? CategoryName { get; set; }
    public string? SearchString { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 9;
}
