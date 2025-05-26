using Noo.Api.Core.Utils.DI;
using Noo.Api.Courses.DTO;

namespace Noo.Api.Courses.Services;

[RegisterScoped(typeof(ICourseService))]
public class CourseService : ICourseService
{
    public Task<CourseDTO?> GetByIdAsync(Ulid id)
    {
        throw new NotImplementedException();
    }
}
