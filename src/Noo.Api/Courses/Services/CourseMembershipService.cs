
using Noo.Api.Core.Utils.DI;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

[RegisterScoped(typeof(ICourseMembershipService))]
public class CourseMembershipService : ICourseMembershipService
{
    public Task<CourseMembershipModel?> GetMembershipAsync(Ulid courseId, Ulid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasAccessAsync(Ulid courseId, Ulid userId)
    {
        throw new NotImplementedException();
    }
}
