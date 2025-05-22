using Noo.Api.Courses.DTO;

namespace Noo.Api.Courses.Services;

public interface ICourseService
{
    public Task<CourseDTO?> GetByIdAsync(Ulid id);
}
