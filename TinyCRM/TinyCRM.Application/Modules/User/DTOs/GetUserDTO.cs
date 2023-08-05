using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.User.DTOs
{
    public class GetUserDTO : GuidBaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}