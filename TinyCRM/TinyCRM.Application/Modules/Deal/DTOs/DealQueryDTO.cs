using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Deal.DTOs
{
    public class DealQueryDTO : DataQueryDTO<DealEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(DealSortByEnum))]
        public DealSortByEnum? SortBy { get; set; }

        public override Expression<Func<DealEntity, bool>> BuildFilterExpression()
        {
            return entity => entity.Title.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? null!;
        }
    }

    public enum DealSortByEnum
    {
        Id = 1,
        Title,
    }
}