using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.API.Common.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Lead.DTOs
{
    public class LeadQueryDTO : DataQueryDTO<LeadEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(LeadSortByEnum))]
        public LeadSortByEnum? SortBy { get; set; }

        public override Expression<Func<LeadEntity, bool>> BuildFilterExpression()
        {
            return entity => entity.Title.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? null!;
        }
    }

    public enum LeadSortByEnum
    {
        Id = 1,
        Title,
    }
}