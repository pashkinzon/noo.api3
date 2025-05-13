using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Core.Utils.DI;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

[RegisterScoped(typeof(IMentorService))]
public class MentorService : IMentorService
{
    private readonly IUnitOfWork _unitOfWork;

    public MentorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<MentorAssignmentDTO> AssignMentorAsync(Ulid studentId, Ulid mentorId, Ulid subjectId)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<MentorAssignmentDTO>, int)> GetAssignmentsAsync(Ulid userId, Criteria<MentorAssignmentModel> criteria)
    {
        throw new NotImplementedException();
    }

    public Task UnassignMentorAsync(Ulid assignmentId)
    {
        throw new NotImplementedException();
    }
}
