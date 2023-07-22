using TinyCRM.Domain.Entities.Enums;

namespace TinyCRM.API.Modules.Product.DTOs
{
    public class GetProductDTO
    {
        public Guid Id { get; set; }
        public string StringId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ProductTypeEnum Type { get; set; }
        public double Price { get; set; }
    }
}