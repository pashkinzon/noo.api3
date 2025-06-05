using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.AssignedWorks;

public class AssignedWorkPolicies : IPolicyRegistrar
{
    public const string CanGetAssignedWorks = nameof(CanGetAssignedWorks);
    public const string CanGetAssignedWork = nameof(CanGetAssignedWork);
    public const string CanGetAssignedWorkProgress = nameof(CanGetAssignedWorkProgress);
    public const string CanRemakeAssignedWork = nameof(CanRemakeAssignedWork);
    public const string CanEditAssignedWork = nameof(CanEditAssignedWork);
    public const string CanCommentAssignedWork = nameof(CanCommentAssignedWork);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetAssignedWorks, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Student),
                nameof(UserRoles.Mentor)
            ).RequireNotBlocked();
        });
    }
}
