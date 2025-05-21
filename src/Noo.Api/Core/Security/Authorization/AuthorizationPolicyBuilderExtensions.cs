using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization.Requirements;

namespace Noo.Api.Core.Security.Authorization;

public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder RequireNotBlocked(this AuthorizationPolicyBuilder builder)
        => builder.AddRequirements(new NotBlockedRequirement());
}
