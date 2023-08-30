using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Core.DTOs;
using TinyCRM.SaleManagement.Application.DTOs.Enums;
using TinyCRM.SaleManagement.Domain.Entities;

namespace TinyCRM.SaleManagement.Application.DTOs;

public class FilterAndPagingDealsDto : FilterAndPagingDto<Deal>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(DealSortProperties))]
    public DealSortProperties? SortBy { get; set; }

    public override Expression<Func<Deal, bool>> BuildSearchExpression()
    {
        return entity => entity.Title.Contains(Name ?? string.Empty);
    }

    public override string BuildSort()
    {
        return SortBy.ToString() ?? null!;
    }
}