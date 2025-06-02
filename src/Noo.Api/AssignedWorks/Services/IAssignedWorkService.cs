using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Criteria;

namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkService
{
    public Task<(IEnumerable<AssignedWorkDTO>, int)> GetStudentAssignedWorksAsync(Ulid studentId, Criteria<AssignedWorkModel> criteria);
    public Task<(IEnumerable<AssignedWorkDTO>, int)> GetMentorAssignedWorksAsync(Ulid mentorId, Criteria<AssignedWorkModel> criteria);
}
