using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Lead.DTOs
{
    public class LeadQueryDTO : DataQueryDto<LeadEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(LeadSortByEnum))]
        public LeadSortByEnum? SortBy { get; set; }

        public override Expression<Func<LeadEntity, bool>> BuildExpression()
        {
            return entity => entity.Title.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? string.Empty;
        }
    }

    public enum LeadSortByEnum
    {
        Id = 1,
        Title,
    }
}