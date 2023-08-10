namespace TinyCRM.Application.Modules.Contact.DTOs;

public class AddOrUpdateContactDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public Guid? AccountId { get; set; }
}