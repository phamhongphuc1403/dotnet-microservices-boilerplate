using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Deal.DTOs;

public class DealQueryDto : DataQueryDto<DealEntity>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(DealSortProperties))]
    public DealSortProperties? SortBy { get; set; }

    public override Expression<Func<DealEntity, bool>> BuildFilterExpression()
    {
        return entity => entity.Title.Contains(Name ?? string.Empty);
    }

    public override string BuildSort()
    {
        return SortBy.ToString() ?? null!;
    }
}

public enum DealSortProperties
{
    Id = 1,
    Title
}