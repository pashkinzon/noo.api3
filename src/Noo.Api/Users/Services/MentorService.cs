using AutoMapper;
using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Criteria.Filters;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

[RegisterScoped(typeof(IMentorService))]
public class MentorService : IMentorService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly MentorAssignmentSearchStrategy _mentorAssignmentSearchStrategy;

    public MentorService(IUnitOfWork unitOfWork, IMapper mapper, MentorAssignmentSearchStrategy mentorAssignmentSearchStrategy)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mentorAssignmentSearchStrategy = mentorAssignmentSearchStrategy;
    }

    public async Task<Ulid> AssignMentorAsync(Ulid studentId, Ulid mentorId, Ulid subjectId)
    {
        var existingAssignment = await _unitOfWork.MentorAssignmentRepository().GetAsync(studentId, mentorId, subjectId);

        if (existingAssignment == null)
        {
            existingAssignment = new MentorAssignmentModel
            {
                StudentId = studentId,
                MentorId = mentorId,
                SubjectId = subjectId
            };

            _unitOfWork.MentorAssignmentRepository().Update(existingAssignment);
            await _unitOfWork.CommitAsync();
        }

        return existingAssignment.Id;
    }

    public Task UnassignMentorAsync(Ulid assignmentId)
    {
        _unitOfWork.MentorAssignmentRepository().DeleteById(assignmentId);
        return _unitOfWork.CommitAsync();
    }

    public async Task<(IEnumerable<MentorAssignmentDTO>, int)> GetMentorAssignmentsAsync(Ulid studentId, Criteria<MentorAssignmentModel> criteria)
    {
        criteria.AddFilter(nameof(MentorAssignmentModel.StudentId), FilterType.Equals, studentId);

        var (items, total) = await _unitOfWork.MentorAssignmentRepository()
            .SearchAsync<MentorAssignmentDTO>(criteria, _mentorAssignmentSearchStrategy, _mapper.ConfigurationProvider);

        return (items, total);
    }

    public async Task<(IEnumerable<MentorAssignmentDTO>, int)> GetStudentAssignmentsAsync(Ulid mntorId, Criteria<MentorAssignmentModel> criteria)
    {
        criteria.AddFilter(nameof(MentorAssignmentModel.MentorId), FilterType.Equals, mntorId);

        var (items, total) = await _unitOfWork.MentorAssignmentRepository()
            .SearchAsync<MentorAssignmentDTO>(criteria, _mentorAssignmentSearchStrategy, _mapper.ConfigurationProvider);

        return (items, total);
    }
}
