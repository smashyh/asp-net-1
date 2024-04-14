namespace Infrastructure.Entities;

public class ContactEntity
{
    public Guid Id { get; set; } = new Guid();
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime DatePosted { get; set; } = DateTime.Now;
}
