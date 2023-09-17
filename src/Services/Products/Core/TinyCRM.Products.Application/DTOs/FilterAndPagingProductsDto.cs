using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using BuildingBlock.Domain.DTOs;
using TinyCRM.Products.Application.DTOs.Enums;

namespace TinyCRM.Products.Application.DTOs;

public class FilterAndPagingProductsDto : FilterAndPagingDto<Domain.Entities.Product>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(ProductSortProperties))]
    public ProductSortProperties? SortBy { get; set; }

    public override Expression<Func<Domain.Entities.Product, bool>> BuildSearchExpression()
    {
        return entity => entity.Name.Contains(Name ?? string.Empty);
    }

    public override string? BuildSort()
    {
        return SortBy.ToString();
    }
}