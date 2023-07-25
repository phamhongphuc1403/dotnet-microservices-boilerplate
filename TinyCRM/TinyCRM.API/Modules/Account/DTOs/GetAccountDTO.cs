using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Account.DTOs
{
    public class GetAccountDto : GuidBaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public double? ToSales { get; set; }
    }
}