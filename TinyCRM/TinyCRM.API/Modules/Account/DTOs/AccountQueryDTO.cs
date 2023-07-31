using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Account.DTOs
{
    public class AccountQueryDTO : DataQueryDTO<AccountEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(AccountSortByEnum))]
        public AccountSortByEnum? SortBy { get; set; }

        public override Expression<Func<AccountEntity, bool>> BuildFilterExpression()
        {
            return entity => entity.Name.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? string.Empty;
        }
    }

    public enum AccountSortByEnum
    {
        Id = 1,
        Name,
        Email,
        PhoneNumber,
        Address,
        ToSales
    }
}