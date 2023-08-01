using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.API.Common.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Contact.DTOs
{
    public class ContactQueryDTO : DataQueryDTO<ContactEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(ContactSortByEnum))]
        public ContactSortByEnum? SortBy { get; set; }

        public override Expression<Func<ContactEntity, bool>> BuildFilterExpression()
        {
            return entity => entity.Name.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? null!;
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