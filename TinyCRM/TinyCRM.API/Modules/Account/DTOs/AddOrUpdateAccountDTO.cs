using System.ComponentModel.DataAnnotations;

namespace TinyCRM.API.Modules.Account.DTOs
{
    public class AddOrUpdateAccountDTO
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}