using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Product.DTOs
{
    public class GetProductDto : GuidBaseEntity
    {
        public string StringId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ProductTypeEnum Type { get; set; }
        public double Price { get; set; }
    }
}