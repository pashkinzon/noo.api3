using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

public interface ICourseRepository : IRepository<CourseModel>
{
    public Task<CourseModel?> GetWithChapterTreeAsync(CourseModel course, int maxDepth = 2);
}
