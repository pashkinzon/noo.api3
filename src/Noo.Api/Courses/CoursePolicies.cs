using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Courses.AuthorizationRequirements;

namespace Noo.Api.Courses;

public class CoursePolicies : IPolicyRegistrar
{
    public const string CanSearchAllCourses = nameof(CanSearchAllCourses);
    public const string CanGetCourse = nameof(CanGetCourse);
    public const string CanPatchCourse = nameof(CanPatchCourse);
    public const string CanAddCourseMembers = nameof(CanAddCourseMembers);
    public const string CanDeleteCourse = nameof(CanDeleteCourse);

    public void RegisterPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(CanGetCourse, policy =>
            policy.AddRequirements(new CourseAccessRequirement()).RequireNotBlocked());

        options.AddPolicy(CanSearchAllCourses, policy =>
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher),
                nameof(UserRoles.Assistant),
                nameof(UserRoles.Mentor)
            ).RequireNotBlocked());

        options.AddPolicy(CanPatchCourse, policy =>
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked());

        options.AddPolicy(CanAddCourseMembers, policy =>
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked());

        options.AddPolicy(CanDeleteCourse, policy =>
            policy.RequireRole(
                nameof(UserRoles.Admin),
                nameof(UserRoles.Teacher)
            ).RequireNotBlocked());
    }
}
