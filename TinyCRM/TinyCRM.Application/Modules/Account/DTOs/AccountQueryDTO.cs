using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Account.DTOs
{
    public class AccountQueryDto : DataQueryDto<AccountEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(AccountSortProperties))]
        public AccountSortProperties? SortBy { get; set; }

        public override Expression<Func<AccountEntity, bool>> BuildFilterExpression()
        {
            return entity => entity.Name.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? string.Empty;
        }
    }

    public enum AccountSortProperties
    {
        Id = 1,
        Name,
        Email,
        PhoneNumber,
        Address,
        ToSales
    }
}