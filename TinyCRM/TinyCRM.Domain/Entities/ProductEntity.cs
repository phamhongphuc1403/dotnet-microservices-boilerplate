using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.Domain.Entities
{
    public class ProductEntity : GuidBaseEntity
    {
        public string StringId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public ProductTypeEnum Type { get; set; }
        public virtual ICollection<DealProductEntity> DealsProducts { get; set; }

        public ProductEntity()
        {
            DealsProducts = new HashSet<DealProductEntity>();
        }
    }
}