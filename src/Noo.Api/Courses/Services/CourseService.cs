using AutoMapper;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

[RegisterScoped(typeof(ICourseService))]
public class CourseService : ICourseService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;
    protected readonly CourseSearchStrategy _searchStrategy;

    public CourseService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        CourseSearchStrategy searchStrategy
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _searchStrategy = searchStrategy;
    }

    public async Task<CourseDTO?> GetByIdAsync(Ulid id, bool includeInactive)
    {
        var course = await _unitOfWork
            .CourseRepository()
            .GetWithChapterTreeAsync(new CourseModel { Id = id });

        return course is null ? null : _mapper.Map<CourseDTO>(course);
    }

    public async Task<(IEnumerable<CourseDTO> courses, int total)> SearchAsync(Criteria<CourseModel> criteria)
    {
        var (courses, total) = await _unitOfWork
            .CourseRepository()
            .SearchAsync<CourseDTO>(criteria, _searchStrategy, _mapper.ConfigurationProvider);

        return (courses, total);
    }
}
