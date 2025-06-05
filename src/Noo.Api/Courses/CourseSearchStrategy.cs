using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses;

[RegisterTransient]
public class CourseSearchStrategy : ISearchStrategy<CourseModel>
{
    public IQueryable<CourseModel> Apply(IQueryable<CourseModel> query, string needle)
    {
        if (string.IsNullOrWhiteSpace(needle))
        {
            return query;
        }

        var lowerNeedle = needle.ToLowerInvariant();

        return query.Where(course =>
            course.Name.ToLowerInvariant().Contains(lowerNeedle));
    }
}
