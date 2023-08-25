using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Core.DTOs;
using TinyCRM.ProductManagement.Application.DTOs.Enums;
using TinyCRM.ProductManagement.Domain.Entities;

namespace TinyCRM.ProductManagement.Application.DTOs;

public class FilterAndPagingProductsDto : FilterAndPagingDto<Product>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(ProductSortProperties))]
    public ProductSortProperties? SortBy { get; set; }

    public override Expression<Func<Product, bool>> BuildSearchExpression()
    {
        return entity => entity.Name.Contains(Name ?? string.Empty);
    }

    public override string? BuildSort()
    {
        return SortBy.ToString();
    }
}