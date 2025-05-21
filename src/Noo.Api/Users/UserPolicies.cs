using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Users;

public class UserPolicies : IPolicyRegistrar
{
    public const string CanGetUser = nameof(CanGetUser);
    public const string CanSearchUsers = nameof(CanSearchUsers);
    public const string CanGetSelf = nameof(CanGetSelf);
    public const string CanPatchSelf = nameof(CanPatchSelf);
    public const string CanDeleteSelf = nameof(CanDeleteSelf);
    public const string CanBlockUser = nameof(CanBlockUser);
    public const string CanChangeRole = nameof(CanChangeRole);
    public const string CanVerifyUser = nameof(CanVerifyUser);
    public const string CanAssignMentor = nameof(CanAssignMentor);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetUser, policy =>
        {
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Mentor),
                nameof(UserRoles.Assistant)
            ).RequireNotBlocked();
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

        options.AddPolicy(CanGetSelf, policy => policy.RequireAuthenticatedUser().RequireNotBlocked());

        options.AddPolicy(CanPatchSelf, policy => policy.RequireAuthenticatedUser().RequireNotBlocked());

        options.AddPolicy(CanDeleteSelf, policy => policy.RequireAuthenticatedUser().RequireNotBlocked());

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
    }
}
