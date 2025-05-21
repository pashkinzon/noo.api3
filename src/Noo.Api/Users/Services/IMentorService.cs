using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Users.DTO;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

public interface IMentorService
{
    public Task<(IEnumerable<MentorAssignmentDTO>, int)> GetMentorAssignmentsAsync(Ulid studentId, Criteria<MentorAssignmentModel> criteria);

    public Task<(IEnumerable<MentorAssignmentDTO>, int)> GetStudentAssignmentsAsync(Ulid mntorId, Criteria<MentorAssignmentModel> criteria);

    public Task<Ulid> AssignMentorAsync(Ulid studentId, Ulid mentorId, Ulid subjectId);

    public Task UnassignMentorAsync(Ulid assignmentId);
}
