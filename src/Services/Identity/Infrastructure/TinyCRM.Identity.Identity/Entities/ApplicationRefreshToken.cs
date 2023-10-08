using BuildingBlock.Domain;

namespace TinyCRM.Identity.Identity.Entities;

public class ApplicationRefreshToken : Entity
{
    public string UserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string DeviceId { get; set; } = null!;
}