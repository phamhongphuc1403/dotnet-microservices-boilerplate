using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.DealProduct.DTOs
{
    public class DealProductDTO : DataQueryDto<DealProductEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(DealProductSortByEnum))]
        public DealProductSortByEnum? SortBy { get; set; }

        public override Expression<Func<DealProductEntity, bool>> BuildExpression()
        {
            return entity => entity.Product.Name.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? string.Empty;
        }
    }

    public enum DealProductSortByEnum
    {
        Id = 1,
        Quantity,
        PricePerUnit,
    }
}