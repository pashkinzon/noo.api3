using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Platform;

public class PlatformPolicies : IPolicyRegistrar
{
    public const string CanViewChangelog = nameof(CanViewChangelog);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanViewChangelog, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Mentor),
                nameof(UserRoles.Assistant)
            ).RequireNotBlocked();
        });
    }
}
