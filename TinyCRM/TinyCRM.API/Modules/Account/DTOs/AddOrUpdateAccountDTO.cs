using System.ComponentModel.DataAnnotations;

namespace TinyCRM.API.Modules.Account.Model
{
    public class AddOrUpdateAccountDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}