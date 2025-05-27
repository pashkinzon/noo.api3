using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

public interface ICourseService
{
    public Task<CourseDTO?> GetByIdAsync(Ulid id, bool includeInactive);
    public Task<(IEnumerable<CourseDTO> courses, int total)> SearchAsync(Criteria<CourseModel> criteria);
}
