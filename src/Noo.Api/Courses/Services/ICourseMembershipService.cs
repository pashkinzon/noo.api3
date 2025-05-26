namespace Noo.Api.Courses.Services;

public interface ICourseMembershipService
{
    public Task<bool> HasAccessAsync(Ulid courseId, Ulid userId);
}
