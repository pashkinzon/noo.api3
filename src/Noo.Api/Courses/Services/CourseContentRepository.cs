using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

public class CourseContentRepository : Repository<CourseMaterialContentModel>, ICourseContentRepository;

public static class CourseContentRepositoryExtensions
{
    public static ICourseContentRepository CourseContentRepository(this IUnitOfWork unitOfWork)
    {
        return new CourseContentRepository
        {
            Context = unitOfWork.Context
        };
    }
}
