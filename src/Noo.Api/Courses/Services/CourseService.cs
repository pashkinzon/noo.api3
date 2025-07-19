using AutoMapper;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Courses.Filters;
using Noo.Api.Courses.Models;

namespace Noo.Api.Courses.Services;

[RegisterScoped(typeof(ICourseService))]
public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICourseRepository _courseRepository;

    private readonly IMapper _mapper;

    public CourseService(
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _courseRepository = unitOfWork.CourseRepository();
        _mapper = mapper;
    }

    public Task<CourseModel?> GetByIdAsync(Ulid id, bool includeInactive)
    {
        return _courseRepository.GetWithChapterTreeAsync(id);
    }

    public Task<SearchResult<CourseModel>> SearchAsync(CourseFilter filter)
    {
        return _courseRepository.SearchAsync(filter);
    }
}
