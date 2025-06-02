using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.AssignedWorks;

public class AssignedWorkPolicies : IPolicyRegistrar
{
    public const string CanGetOwnAssignedWorks = nameof(CanGetOwnAssignedWorks);
    public const string CanGetAssignedWorksOfOthers = nameof(CanGetAssignedWorksOfOthers);
    public const string CanViewAssignedWork = nameof(CanViewAssignedWork);
    public const string CanSolveAssignedWork = nameof(CanSolveAssignedWork);
    public const string CanCheckAssignedWork = nameof(CanCheckAssignedWork);
    public const string CanRemakeAssignedWork = nameof(CanRemakeAssignedWork);
    public const string CanRecheckAssignedWork = nameof(CanRecheckAssignedWork);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetOwnAssignedWorks, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Student),
                nameof(UserRoles.Mentor)
            ).RequireNotBlocked();
        });
    }
}
