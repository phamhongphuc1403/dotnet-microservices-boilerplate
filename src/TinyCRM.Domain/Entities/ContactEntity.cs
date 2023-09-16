namespace TinyCRM.Domain.Entities;

public class ContactEntity : GuidBaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public Guid? AccountId { get; set; }
    public virtual AccountEntity? Account { get; set; }
}