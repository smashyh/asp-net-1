namespace Infrastructure.Entities
{
    public class CourseCategoryEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CategoryName { get; set; } = null!;
    }
}
