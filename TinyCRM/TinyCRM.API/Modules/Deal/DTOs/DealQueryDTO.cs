using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Deal.DTOs
{
    public class DealQueryDTO : DataQueryDto<DealEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(DealSortByEnum))]
        public DealSortByEnum? SortBy { get; set; }

        public override Expression<Func<DealEntity, bool>> BuildExpression()
        {
            return entity => entity.Title.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? string.Empty;
        }
    }

    public enum DealSortByEnum
    {
        Id = 1,
        Title,
    }
}
