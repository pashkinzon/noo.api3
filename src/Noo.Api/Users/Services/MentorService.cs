using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Criteria.Filters;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

[RegisterScoped(typeof(IMentorService))]
public class MentorService : IMentorService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly ISearchStrategy<MentorAssignmentModel> _mentorAssignmentSearchStrategy;

    public MentorService(IUnitOfWork unitOfWork, MentorAssignmentSearchStrategy mentorAssignmentSearchStrategy)
    {
        _unitOfWork = unitOfWork;
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

    public Task<SearchResult<MentorAssignmentModel>> GetMentorAssignmentsAsync(Ulid studentId, Criteria<MentorAssignmentModel> criteria)
    {
        criteria.AddFilter("StudentId", FilterType.Equals, studentId);

        return _unitOfWork.MentorAssignmentRepository()
            .SearchAsync(criteria, _mentorAssignmentSearchStrategy);
    }

    public Task<SearchResult<MentorAssignmentModel>> GetStudentAssignmentsAsync(Ulid mentorId, Criteria<MentorAssignmentModel> criteria)
    {
        criteria.AddFilter("MentorId", FilterType.Equals, mentorId);

        return _unitOfWork.MentorAssignmentRepository()
            .SearchAsync(criteria, _mentorAssignmentSearchStrategy);
    }
}
