using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Works;

public class WorkPolicies : IPolicyRegistrar
{
    public const string CanSearchWorks = nameof(CanSearchWorks);
    public const string CanGetWork = nameof(CanGetWork);
    public const string CanSeeWorkStatistics = nameof(CanSeeWorkStatistics);
    public const string CanSeeWorkRelatedMaterials = nameof(CanSeeWorkRelatedMaterials);
    public const string CanCreateWorks = nameof(CanCreateWorks);
    public const string CanEditWorks = nameof(CanEditWorks);
    public const string CanDeleteWorks = nameof(CanDeleteWorks);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanSearchWorks, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Mentor),
                nameof(UserRoles.Assistant)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanGetWork, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Mentor),
                nameof(UserRoles.Assistant)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanSeeWorkStatistics, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanSeeWorkRelatedMaterials, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanCreateWorks, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanEditWorks, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanDeleteWorks, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked();
        });
    }
}
