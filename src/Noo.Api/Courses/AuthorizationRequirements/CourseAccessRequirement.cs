using Microsoft.AspNetCore.Authorization;
using Noo.Api.Core.Security.Authorization;

namespace Noo.Api.Courses.AuthorizationRequirements;

public class CourseAccessRequirement : IAuthorizationRequirement
{
    public IEnumerable<UserRoles> AlwaysAllowedRoles { get; } = [
        UserRoles.Admin,
        UserRoles.Teacher,
        UserRoles.Assistant,
        UserRoles.Mentor
    ];
}
