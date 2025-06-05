using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.UserSettings;

public class UserSettingsPolicies : IPolicyRegistrar
{
    public const string CanGetUserSettings = nameof(CanGetUserSettings);
    public const string CanPatchUserSettings = nameof(CanPatchUserSettings);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetUserSettings, policy =>
        {
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });

        options.AddPolicy(CanPatchUserSettings, policy =>
        {
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });
    }
}
