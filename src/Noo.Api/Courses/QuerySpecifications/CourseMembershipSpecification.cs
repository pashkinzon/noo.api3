using Ardalis.Specification;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.QuerySpecifications;

public class CourseMembershipSpecification : Specification<CourseMembershipModel>
{
    public CourseMembershipSpecification(UserRoles? userRole, Ulid? userId)
    {
        if (userRole == null || userId == null)
        {
            // If no user role or ID is provided, return no memberships
            Query.Where(_ => false);
            return;
        }

        switch (userRole)
        {
            case UserRoles.Admin:
            case UserRoles.Teacher:
            case UserRoles.Assistant:
                // Admins, Teachers, and Assistants can see all memberships
                Query.Where(_ => true);
                break;

            case UserRoles.Mentor:
                // Mentors can see their own memberships
                // TODO: Let mentor see only memberships of the students they mentor
                break;

            case UserRoles.Student:
                // Students can see their own memberships
                Query.Where(membership => membership.StudentId == userId);
                break;

            default:
                // For any other roles, no memberships are visible
                Query.Where(_ => false);
                break;
        }
    }
}
