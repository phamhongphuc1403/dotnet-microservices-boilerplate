namespace TinyCRM.API.Modules.Contact.DTOs
{
    public class GetContactDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public Guid? AccountId { get; set; }
    }
}
