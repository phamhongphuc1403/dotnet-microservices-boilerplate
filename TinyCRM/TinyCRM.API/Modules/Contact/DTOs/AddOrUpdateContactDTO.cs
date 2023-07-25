using System.ComponentModel.DataAnnotations;

namespace TinyCRM.API.Modules.Contact.DTOs
{
    public class AddOrUpdateContactDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        public Guid? AccountId { get; set; }
    }
}