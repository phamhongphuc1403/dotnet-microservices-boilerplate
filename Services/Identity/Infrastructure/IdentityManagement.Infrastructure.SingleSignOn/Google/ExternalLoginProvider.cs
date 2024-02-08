using AutoMapper;
using IdentityManagement.Core.Application.Users;
using IdentityManagement.Core.Application.Users.DTOs.Enums;
using Microsoft.Extensions.Configuration;

namespace IdentityManagement.Infrastructure.Google;

public static class ExternalLoginProvider
{
    public static IExternalLoginService GetLoginProvider(AuthProvider authProvider, IMapper mapper,
        IConfiguration configuration)
    {
        {
            return authProvider switch
            {
                AuthProvider.Google => new GoogleExternalLoginService(mapper, configuration),
                _ => throw new ArgumentOutOfRangeException(nameof(authProvider), authProvider, null)
            };
        }
    }
}