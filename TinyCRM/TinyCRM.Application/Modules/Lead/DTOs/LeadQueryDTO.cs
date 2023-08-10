using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Lead.DTOs;

public class LeadQueryDto : DataQueryDto<LeadEntity>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(LeadSortProperties))]
    public LeadSortProperties? SortBy { get; set; }

    public override Expression<Func<LeadEntity, bool>> BuildFilterExpression()
    {
        return entity => entity.Title.Contains(Name ?? string.Empty);
    }

    public override string BuildSort()
    {
        return SortBy.ToString() ?? null!;
    }
}

public enum LeadSortProperties
{
    Id = 1,
    Title
}