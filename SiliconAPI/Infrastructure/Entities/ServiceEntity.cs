namespace Infrastructure.Entities;

public class ServiceEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ServiceName { get; set; } = null!;
}
