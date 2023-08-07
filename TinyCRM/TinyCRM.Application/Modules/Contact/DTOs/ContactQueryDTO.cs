using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TinyCRM.Domain.DTOs;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Contact.DTOs
{
    public class ContactQueryDto : DataQueryDto<ContactEntity>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(ContactSortProperties))]
        public ContactSortProperties? SortBy { get; set; }

        public override Expression<Func<ContactEntity, bool>> BuildFilterExpression()
        {
            return entity => entity.Name.Contains(Name ?? string.Empty);
        }

        public override string BuildSort()
        {
            return SortBy.ToString() ?? null!;
        }
    }

    public enum ContactSortProperties
    {
        Id = 1,
        Name,
        Email,
        PhoneNumber
    }
}