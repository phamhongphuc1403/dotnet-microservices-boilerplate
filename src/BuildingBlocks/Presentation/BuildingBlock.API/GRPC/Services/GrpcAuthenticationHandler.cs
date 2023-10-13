using System.Security.Claims;
using System.Text.Encodings.Web;
using BuildingBlock.API.Authentication;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BuildingBlock.API.GRPC.Services;

public class GrpcAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly AuthProvider.AuthProviderClient _authProviderClient;

    public GrpcAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, AuthProvider.AuthProviderClient authProviderClient) : base(options,
        logger, encoder, clock)
    {
        _authProviderClient = authProviderClient;
    }

    private string GetAccessTokenFromHeader()
    {
        if (Context.Request.Headers.ContainsKey("Authorization"))
            return Context.Request.Headers["Authorization"].ToString();

        throw new Exception("Access token not provided!");
    }

    private async Task<List<Claim>> GetClaimsFromIdentityServiceAsync(Metadata headers)
    {
        try
        {
            var claims = new List<Claim>();

            using var response = _authProviderClient.GetClaimsAsync(new ClaimRequest(), new CallOptions(headers));

            await foreach (var claim in response.ResponseStream.ReadAllAsync())
                claims.Add(new Claim(claim.Type, claim.Value));

            return claims;
        }
        catch (RpcException ex)
        {
            return ex.StatusCode switch
            {
                StatusCode.Unauthenticated => throw new Exception("Invalid token!"),
                StatusCode.NotFound => throw new Exception("User not found!"),
                StatusCode.Unavailable => throw new Exception("Identity service is down!"),
                _ => throw new Exception("Unknown error when processing authentication!")
            };
        }
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var accessToken = GetAccessTokenFromHeader();

            var headers = new Metadata
            {
                { "Authorization", accessToken }
            };

            var claims = await GetClaimsFromIdentityServiceAsync(headers);

            return AuthenticateResult.Success(GetTicket(claims));
        }
        catch (Exception exception)
        {
            return AuthenticateResult.Fail(exception.Message);
        }
    }

    private AuthenticationTicket GetTicket(IEnumerable<Claim> claims)
    {
        var identity = new ClaimsIdentity(AuthenticationDefaults.AuthenticationScheme);

        identity.AddClaims(claims);

        return new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
    }
}