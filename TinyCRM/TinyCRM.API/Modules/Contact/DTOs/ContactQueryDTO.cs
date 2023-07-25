using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.PaginationHelper;

namespace TinyCRM.API.Modules.Contact.DTOs
{
    public class ContactQueryDTO : DataQueryDto<ContactEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(ContactSortByEnum))]
        public ContactSortByEnum? SortBy { get; set; }

        public override Expression<Func<ContactEntity, bool>> BuildExpression()
        {
            return entity => entity.Name.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? string.Empty;
        }
    }

    public enum ContactSortByEnum
    {
        Id = 1,
        Name,
        Email,
        PhoneNumber
    }
}
