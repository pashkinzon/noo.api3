using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

public interface IMentorService
{
    public Task<(IEnumerable<MentorAssignmentDTO>, int)> GetAssignmentsAsync(Ulid userId, Criteria<MentorAssignmentModel> criteria);

    public Task<MentorAssignmentDTO> AssignMentorAsync(Ulid studentId, Ulid mentorId, Ulid subjectId);

    public Task UnassignMentorAsync(Ulid assignmentId);
}
