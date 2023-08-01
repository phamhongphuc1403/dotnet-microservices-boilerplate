﻿using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Product.DTOs
{
    public class ProductQueryDTO : DataQueryDTO<ProductEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(ProductSortByEnum))]
        public ProductSortByEnum? SortBy { get; set; }

        public override Expression<Func<ProductEntity, bool>> BuildFilterExpression()
        {
            return entity => entity.Name.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? null!;
        }
    }

    public enum ProductSortByEnum
    {
        Id = 1,
        StringId,
        Name,
        Price,
    }
}