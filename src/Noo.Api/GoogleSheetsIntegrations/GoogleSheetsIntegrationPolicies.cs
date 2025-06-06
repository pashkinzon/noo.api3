using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.GoogleSheetsIntegrations;

public class GoogleSheetsIntegrationPolicies : IPolicyRegistrar
{
    public const string CanGetGoogleSheetsIntegrations = nameof(CanGetGoogleSheetsIntegrations);
    public const string CanCreateGoogleSheetsIntegration = nameof(CanCreateGoogleSheetsIntegration);
    public const string CanDeleteGoogleSheetsIntegration = nameof(CanDeleteGoogleSheetsIntegration);
    public const string CanRunGoogleSheetsIntegration = nameof(CanRunGoogleSheetsIntegration);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetGoogleSheetsIntegrations, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanCreateGoogleSheetsIntegration, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanDeleteGoogleSheetsIntegration, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanRunGoogleSheetsIntegration, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });
    }
}
