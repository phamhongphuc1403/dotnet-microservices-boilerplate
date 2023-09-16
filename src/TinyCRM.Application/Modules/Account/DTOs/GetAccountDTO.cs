using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Account.DTOs;

public class GetAccountDto : GuidBaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public double? ToSales { get; set; }
}