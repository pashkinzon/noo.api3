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
                UserRoles.Admin.ToString(),
                UserRoles.Teacher.ToString(),
                UserRoles.Mentor.ToString(),
                UserRoles.Assistant.ToString()
            );
        });

        options.AddPolicy(CanGetWork, policy =>
        {
            policy.RequireRole(
                UserRoles.Admin.ToString(),
                UserRoles.Teacher.ToString(),
                UserRoles.Mentor.ToString(),
                UserRoles.Assistant.ToString()
            );
        });

        options.AddPolicy(CanSeeWorkStatistics, policy =>
        {
            policy.RequireRole(
                UserRoles.Admin.ToString(),
                UserRoles.Teacher.ToString()
            );
        });

        options.AddPolicy(CanSeeWorkRelatedMaterials, policy =>
        {
            policy.RequireRole(
                UserRoles.Admin.ToString(),
                UserRoles.Teacher.ToString()
            );
        });

        options.AddPolicy(CanCreateWorks, policy =>
        {
            policy.RequireRole(
                UserRoles.Admin.ToString(),
                UserRoles.Teacher.ToString()
            );
        });

        options.AddPolicy(CanEditWorks, policy =>
        {
            policy.RequireRole(
                UserRoles.Admin.ToString(),
                UserRoles.Teacher.ToString()
            );
        });

        options.AddPolicy(CanDeleteWorks, policy =>
        {
            policy.RequireRole(
                UserRoles.Admin.ToString(),
                UserRoles.Teacher.ToString()
            );
        });
    }
}
