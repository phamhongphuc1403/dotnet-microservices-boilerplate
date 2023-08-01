using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TinyCRM.API.Common.Constants;

namespace TinyCRM.API.Extensions
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection AddAuthorizationExtension(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewAndUpdateUserPermission", policy =>
                    policy.Requirements.Add(new EditInfoRequirement()));
            });
            return services;
        }
    }

    public class EditInfoRequirement : IAuthorizationRequirement
    {
    }

    public class ViewOrUpdateUserHandler : AuthorizationHandler<EditInfoRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ViewOrUpdateUserHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, EditInfoRequirement requirement)

        {
            var httpContext = _httpContextAccessor.HttpContext;

            string? idValue = httpContext?.Request.RouteValues["id"] as string;

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if ((idValue != null && idValue == userId) || context.User.IsInRole(Role.Admin))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}