
using Noo.Api.Core.Utils.DI;

namespace Noo.Api.Courses.Services;

[RegisterScoped(typeof(ICourseMembershipService))]
public class CourseMembershipService : ICourseMembershipService
{
    public Task<bool> HasAccessAsync(Ulid courseId, Ulid userId)
    {
        throw new NotImplementedException();
    }
}
