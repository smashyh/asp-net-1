namespace Infrastructure.Entities;

public class AddressEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    public string Address_1 { get; set; } = null!; 
    public string? Address_2 { get; set; }
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
}