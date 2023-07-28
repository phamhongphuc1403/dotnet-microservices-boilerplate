namespace TinyCRM.Domain.Entities
{
    public partial class AccountEntity : GuidBaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public double? ToSales { get; set; }
        public virtual ICollection<ContactEntity> Contacts { get; set; }
        public virtual ICollection<LeadEntity> Leads { get; set; }

        public AccountEntity()
        {
            Contacts = new HashSet<ContactEntity>();
            Leads = new HashSet<LeadEntity>();
        }
    }
}