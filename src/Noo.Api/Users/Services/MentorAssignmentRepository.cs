using Microsoft.EntityFrameworkCore;
using Noo.Api.Core.DataAbstraction.Db;
using Noo.Api.Users.Models;

namespace Noo.Api.Users.Services;

public class MentorAssignmentRepository : Repository<MentorAssignmentModel>, IMentorAssignmentRepository
{
    public Task<MentorAssignmentModel?> GetAsync(Ulid studentId, Ulid mentorId, Ulid subjectId)
    {
        return Context.GetDbSet<MentorAssignmentModel>()
            .FirstOrDefaultAsync(x =>
                x.StudentId == studentId &&
                x.MentorId == mentorId &&
                x.SubjectId == subjectId
            );
    }
}

public static class UnitIOfWorkMentorAssignmentRepositoryExtensions
{
    public static IMentorAssignmentRepository MentorAssignmentRepository(this IUnitOfWork unitOfWork)
    {
        return new MentorAssignmentRepository()
        {
            Context = unitOfWork.Context
        };
    }
}
