using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TinyCRM.Application.Modules.User.DTOs
{
    public class UserQueryDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(UserSortProperties))]
        public UserSortProperties? SortBy { get; set; }

        public string BuildSort()
        {
            return SortBy.ToString() ?? null!;
        }

        [Range(1, int.MaxValue, ErrorMessage = "The value must be larger than 0.")]
        public int? Take { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The value must be larger than 0.")]
        public int? Page { get; set; }

        public string? Name { get; set; }
        public bool? IsDescending { get; set; }
    }

    public enum UserSortProperties
    {
        Id = 1,
        Name,
        Email
    }
}
