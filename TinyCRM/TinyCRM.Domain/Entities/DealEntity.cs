using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.Domain.Entities
{
    public class DealEntity : GuidBaseEntity
    {
        public string Title { get; set; } = null!;
        public Guid LeadId { get; set; }
        public virtual LeadEntity Lead { get; set; }
        public string? Description { get; set; }
        public DealStatusEnum Status { get; set; }
        public virtual ICollection<DealProductEntity> DealsProducts { get; set; }

        public DealEntity()
        {
            DealsProducts = new HashSet<DealProductEntity>();
        }
    }
}