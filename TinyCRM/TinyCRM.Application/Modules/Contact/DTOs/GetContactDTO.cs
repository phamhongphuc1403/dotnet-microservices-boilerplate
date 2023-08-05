using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Contact.DTOs
{
    public class GetContactDTO : GuidBaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public Guid? AccountId { get; set; }
    }
}