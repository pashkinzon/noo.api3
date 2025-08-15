using AutoMapper;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Security.Authorization;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Courses.DTO;
using Noo.Api.Courses.Filters;
using Noo.Api.Courses.Models;
using Noo.Api.Courses.QuerySpecifications;
using SystemTextJsonPatch;

namespace Noo.Api.Courses.Services;

[RegisterScoped(typeof(ICourseService))]
public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ICourseRepository _courseRepository;

    private readonly ICourseContentRepository _courseContentRepository;

    private readonly ICurrentUser _currentUser;

    private readonly IMapper _mapper;

    public CourseService(
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _courseRepository = unitOfWork.CourseRepository();
        _courseContentRepository = unitOfWork.CourseContentRepository();
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<Ulid> CreateAsync(CreateCourseDTO dto)
    {
        var model = _mapper.Map<CourseModel>(dto);

        _courseRepository.Add(model);
        await _unitOfWork.CommitAsync();

        return model.Id;
    }

    public async Task<Ulid> CreateMaterialContentAsync(CreateCourseMaterialContentDTO dto)
    {
        var model = _mapper.Map<CourseMaterialContentModel>(dto);

        _courseContentRepository.Add(model);
        await _unitOfWork.CommitAsync();

        return model.Id;
    }

    public Task<CourseModel?> GetByIdAsync(Ulid id, bool includeInactive)
    {
        return _courseRepository.GetWithChapterTreeAsync(id, includeInactive);
    }

    public Task<CourseMaterialContentModel?> GetContentByIdAsync(Ulid contentId)
    {
        return _courseContentRepository.GetByIdAsync(contentId);
    }

    public Task<SearchResult<CourseModel>> SearchAsync(CourseFilter filter)
    {
        return _courseRepository.SearchAsync(filter, [
            new CourseSpecification(_currentUser.UserRole, _currentUser.UserId)
        ]);
    }

    public async Task SoftDeleteAsync(Ulid courseId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null) return;

        course.IsDeleted = true;
        _courseRepository.Update(course);

        await _unitOfWork.CommitAsync();
    }

    public Task UpdateAsync(Ulid courseId, JsonPatchDocument<UpdateCourseDTO> courseUpdateDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateContentAsync(Ulid contentId, JsonPatchDocument<UpdateCourseMaterialContentDTO> contentUpdateDto)
    {
        throw new NotImplementedException();
    }
}
