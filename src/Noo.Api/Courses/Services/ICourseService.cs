using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Courses.Filters;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

public interface ICourseService
{
    public Task<CourseModel?> GetByIdAsync(Ulid id, bool includeInactive);
    public Task<SearchResult<CourseModel>> SearchAsync(CourseFilter criteria);
}
