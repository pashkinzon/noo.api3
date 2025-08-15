using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

public class CourseMembershipRepository : Repository<CourseMembershipModel>, ICourseMembershipRepository
{
    public Task<CourseMembershipModel?> GetMembershipAsync(Ulid courseId, Ulid userId)
    {
        return Context.Set<CourseMembershipModel>()
            .Where(m => m.CourseId == courseId && m.StudentId == userId)
            .FirstOrDefaultAsync();
    }
}

public static class CourseMembershipRepositoryExtensions
{
    public static ICourseMembershipRepository CourseMembershipRepository(this IUnitOfWork unitOfWork)
    {
        return new CourseMembershipRepository
        {
            Context = unitOfWork.Context
        };
    }
}
