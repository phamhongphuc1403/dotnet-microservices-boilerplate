namespace TinyCRM.Domain.Entities
{
    public class ContactEntity : GuidBaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public Guid? AccountId { get; set; }
        public virtual AccountEntity? Account { get; set; }
    }
}
