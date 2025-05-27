using Noo.Api.Courses.DTO;

namespace Noo.Api.Courses.Services;

public interface ICourseMembershipService
{
    public Task<bool> HasAccessAsync(Ulid courseId, Ulid userId);
    public Task<CourseMembershipDTO?> GetMembershipAsync(Ulid courseId, Ulid userId);
}
