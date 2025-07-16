using Noo.Api.Core.DataAbstraction.Criteria;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

public interface IMentorService
{
    public Task<SearchResult<MentorAssignmentModel>> GetMentorAssignmentsAsync(Ulid studentId, Criteria<MentorAssignmentModel> criteria);

    public Task<SearchResult<MentorAssignmentModel>> GetStudentAssignmentsAsync(Ulid mntorId, Criteria<MentorAssignmentModel> criteria);

    public Task<Ulid> AssignMentorAsync(Ulid studentId, Ulid mentorId, Ulid subjectId);

    public Task UnassignMentorAsync(Ulid assignmentId);
}
