using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

public interface ICourseMembershipService
{
    public Task<bool> HasAccessAsync(Ulid courseId, Ulid userId);
    public Task<CourseMembershipModel?> GetMembershipAsync(Ulid courseId, Ulid userId);
}
