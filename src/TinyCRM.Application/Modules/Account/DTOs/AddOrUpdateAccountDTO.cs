namespace TinyCRM.Application.Modules.Account.DTOs;

public class AddOrUpdateAccountDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}