using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Product.DTOs
{
    public class GetProductDTO : GuidBaseEntity
    {
        public string StringId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ProductTypeEnum Type { get; set; }
        public double Price { get; set; }
    }
}