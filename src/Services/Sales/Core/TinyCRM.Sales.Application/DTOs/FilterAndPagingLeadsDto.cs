using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using BuildingBlock.Domain.DTOs;
using TinyCRM.Sales.Application.DTOs.Enums;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Application.DTOs;

public class FilterAndPagingLeadsDto : FilterAndPagingDto<Lead>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(LeadSortProperties))]
    public LeadSortProperties? SortBy { get; set; }

    public override Expression<Func<Lead, bool>> BuildSearchExpression()
    {
        return entity => entity.Title.Contains(Name ?? string.Empty);
    }

    public override string BuildSort()
    {
        return SortBy.ToString() ?? null!;
    }
}