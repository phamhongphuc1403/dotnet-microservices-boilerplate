using BuildingBlock.Core.Domain.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BuildingBlock.Presentation.API.Authorization;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    private DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return FallbackPolicyProvider.GetFallbackPolicyAsync();
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        return FallbackPolicyProvider.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(Permissions.Prefix, StringComparison.OrdinalIgnoreCase))
            return FallbackPolicyProvider.GetPolicyAsync(policyName);

        var policy = new AuthorizationPolicyBuilder();

        policy.AddRequirements(new PermissionRequirement(policyName));

        return Task.FromResult(policy.Build())!;
    }
}