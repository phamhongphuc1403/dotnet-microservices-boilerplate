using System.Security.Authentication;
using AutoMapper;
using BuildingBlock.Presentation.API.Utilities;
using Google.Apis.Auth;
using IdentityManagement.Core.Application.Users;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using Microsoft.Extensions.Configuration;

namespace IdentityManagement.Infrastructure.Google;

public class GoogleExternalLoginService : IExternalLoginService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public GoogleExternalLoginService(IMapper mapper, IConfiguration configuration)
    {
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<User> ValidateAsync(string token)
    {
        try
        {
            var clientId = _configuration.GetRequiredValue("SingleSignOn:Google:ClientId");

            var validationSettings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { clientId }
            };

            var validPayload = await GoogleJsonWebSignature.ValidateAsync(token, validationSettings);

            if (validPayload == null) throw new Exception("Invalid token");

            return _mapper.Map<User>(validPayload);
        }
        catch (Exception ex)
        {
            throw new AuthenticationException(ex.Message);
        }
    }
}