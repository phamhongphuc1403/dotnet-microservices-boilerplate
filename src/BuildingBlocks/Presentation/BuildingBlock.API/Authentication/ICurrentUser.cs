using System.Security.Claims;

namespace BuildingBlock.API.Authentication;

public interface ICurrentUser
{
    public string? Id { get; }
    public string? Name { get; }
    public string? Email { get; }
    public bool IsAuthenticated { get; }
    public ClaimsPrincipal? ClaimPrincipal { get; }

    bool IsInRole(string role);

    string? GetClaim(string claimType);
}