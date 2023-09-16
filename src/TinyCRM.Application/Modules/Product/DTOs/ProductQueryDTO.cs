using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Product.DTOs;

public class ProductQueryDto : DataQueryDto<ProductEntity>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(ProductSortProperties))]
    public ProductSortProperties? SortBy { get; set; }

    public override Expression<Func<ProductEntity, bool>> BuildFilterExpression()
    {
        return entity => entity.Name.Contains(Name ?? string.Empty);
    }

    public override string BuildSort()
    {
        return SortBy.ToString() ?? null!;
    }
}

public enum ProductSortProperties
{
    Id = 1,
    StringId,
    Name,
    Price
}