using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Application.Common.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.DealProduct.DTOs
{
    public class DealProductDTO : DataQueryDTO<DealProductEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(DealProductSortByEnum))]
        public DealProductSortByEnum? SortBy { get; set; }

        public override Expression<Func<DealProductEntity, bool>> BuildFilterExpression()
        {
            return entity => entity.Product.Name.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? null!;
        }
    }

    public enum DealProductSortByEnum
    {
        Id = 1,
        Quantity,
        PricePerUnit,
    }
}