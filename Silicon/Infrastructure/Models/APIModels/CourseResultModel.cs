namespace Infrastructure.Models.APIModels;

public class CourseResultModel
{
    public int TotalItemCount { get; set; }
    public int TotalPageCount { get; set; }
    public IEnumerable<CourseModel> Courses { get; set; } = null!;
}
