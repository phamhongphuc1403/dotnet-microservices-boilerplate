using IdentityManagement.Core.Application.Users.DTOs.Enums;

namespace IdentityManagement.Core.Application.Users.DTOs;

public class ExternalLoginDto
{
    public AuthProvider AuthProvider { get; set; }

    public string Token { get; set; } = null!;
}