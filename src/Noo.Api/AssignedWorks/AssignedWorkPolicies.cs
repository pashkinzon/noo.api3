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
    public const string CanSolveAssignedWork = nameof(CanSolveAssignedWork);
    public const string CanCheckAssignedWork = nameof(CanCheckAssignedWork);
    public const string CanArchiveAssignedWork = nameof(CanArchiveAssignedWork);
    public const string CanUnarchiveAssignedWork = nameof(CanUnarchiveAssignedWork);
    public const string CanAddHelperMentorToAssignedWork = nameof(CanAddHelperMentorToAssignedWork);
    public const string CanReplaceMainMentorOfAssignedWork = nameof(CanReplaceMainMentorOfAssignedWork);
    public const string CanShiftDeadlineOfAssignedWork = nameof(CanShiftDeadlineOfAssignedWork);
    public const string CanReturnAssignedWorkToSolve = nameof(CanReturnAssignedWorkToSolve);
    public const string CanReturnAssignedWorkToCheck = nameof(CanReturnAssignedWorkToCheck);
    public const string CanDeleteAssignedWork = nameof(CanDeleteAssignedWork);

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
