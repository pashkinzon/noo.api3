using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Users;

public class UserPolicies : IPolicyRegistrar
{
    public const string CanGetUser = nameof(CanGetUser);
    public const string CanPatchUser = nameof(CanPatchUser);
    public const string CanSearchUsers = nameof(CanSearchUsers);
    public const string CanBlockUser = nameof(CanBlockUser);
    public const string CanChangeRole = nameof(CanChangeRole);
    public const string CanVerifyUser = nameof(CanVerifyUser);
    public const string CanAssignMentor = nameof(CanAssignMentor);
    public const string CanDeleteUser = nameof(CanDeleteUser);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        // TODO: Change so that every user can get their own data
        options.AddPolicy(CanGetUser, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Mentor),
                nameof(UserRoles.Assistant)
            ).RequireNotBlocked();
        });

        // TODO: Change so that every user can patch only own data
        options.AddPolicy(CanPatchUser, policy =>
        {
            policy.RequireAuthenticatedUser().RequireNotBlocked();
        });

        options.AddPolicy(CanSearchUsers, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Mentor),
                nameof(UserRoles.Assistant)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanBlockUser, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Admin)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanChangeRole, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Admin)
            ).RequireNotBlocked();
        });

        options.AddPolicy(CanVerifyUser, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Admin)
            ).RequireNotBlocked();
        });

        // TODO: Change so that mentors can assign too but only to themselves
        options.AddPolicy(CanAssignMentor, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Admin)
            ).RequireNotBlocked();
        });

        // TODO: Add also an options for a user to delete their own account
        options.AddPolicy(CanDeleteUser, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin)
            ).RequireNotBlocked();
        });
    }
}
