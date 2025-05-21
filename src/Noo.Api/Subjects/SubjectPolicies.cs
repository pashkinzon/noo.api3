using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Subjects;

public class SubjectPolicies : IPolicyRegistrar
{
    public const string CanGetSubjects = nameof(CanGetSubjects);
    public const string CanGetSubject = nameof(CanGetSubject);
    public const string CanCreateSubject = nameof(CanCreateSubject);
    public const string CanPatchSubject = nameof(CanPatchSubject);
    public const string CanDeleteSubject = nameof(CanDeleteSubject);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetSubjects, policy => policy.RequireNotBlocked());

        options.AddPolicy(CanGetSubject, policy => policy.RequireNotBlocked());

        options.AddPolicy(CanCreateSubject, policy => policy.RequireRole(nameof(UserRoles.Admin)).RequireNotBlocked());

        options.AddPolicy(CanPatchSubject, policy => policy.RequireRole(nameof(UserRoles.Admin)).RequireNotBlocked());

        options.AddPolicy(CanDeleteSubject, policy => policy.RequireRole(nameof(UserRoles.Admin)).RequireNotBlocked());
    }
}
