using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Contact.DTOs
{
    public class GetContactDto : GuidBaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public Guid? AccountId { get; set; }
    }
}