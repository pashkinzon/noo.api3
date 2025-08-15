using Microsoft.EntityFrameworkCore;
using Noo.Api.AssignedWorks.DTO;
using Noo.Api.AssignedWorks.Models;
using Noo.Api.AssignedWorks.Types;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public class AssignedWorkRepository : Repository<AssignedWorkModel>, IAssignedWorkRepository
{
    public Task<AssignedWorkProgressDTO?> GetProgressAsync(Ulid assignedWorkId, Ulid? userId)
    {
        return Context.Set<AssignedWorkModel>()
            .Where(aw => aw.Id == assignedWorkId && aw.StudentId == userId)
            .Select(aw => new AssignedWorkProgressDTO
            {
                AssignedWorkId = aw.Id,
                SolveStatus = aw.SolveStatus,
                SolvedAt = aw.SolvedAt,
                CheckStatus = aw.CheckStatus,
                CheckedAt = aw.CheckedAt,
                Score = aw.Score,
                MaxScore = aw.MaxScore
            })
            .FirstOrDefaultAsync();
    }

    public async Task<AssignedWorkModel?> GetAsync(Ulid assignedWorkId)
    {
        var assignedWork = await Context.Set<AssignedWorkModel>()
            .Where(aw => aw.Id == assignedWorkId)
            .Include(aw => aw.Answers)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (assignedWork?.Answers != null)
        {
            foreach (var answer in assignedWork.Answers.Where(a => a.Status == AssignedWorkAnswerStatus.NotSubmitted))
            {
                // Hide mentor-only fields for not submitted answers
                answer.MentorComment = null;
                answer.Score = null;
                answer.DetailedScore = null;
            }
        }

        return assignedWork;
    }

    public Task<AssignedWorkModel?> GetAsync(Ulid assignedWorkId, Ulid? userId)
    {
        if (userId == null)
        {
            return Task.FromResult<AssignedWorkModel?>(null);
        }

        return Context.Set<AssignedWorkModel>()
            .Where(aw => aw.Id == assignedWorkId && (
                aw.StudentId == userId ||
                aw.MainMentorId == userId ||
                aw.HelperMentorId == userId))
            .FirstOrDefaultAsync();
    }

    public async Task<AssignedWorkModel?> GetWithTasksAndAnswersAsync(Ulid assignedWorkId)
    {
        var assignedWork = await Context.Set<AssignedWorkModel>()
            .Where(aw => aw.Id == assignedWorkId)
            .Include(aw => aw.Answers)
            .Include(aw => aw.Work)
            .ThenInclude(w => w!.Tasks)
            .AsSplitQuery() //! To avoid Cartesian product issues, DO NOT REMOVE
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (assignedWork?.Answers != null)
        {
            foreach (var answer in assignedWork.Answers.Where(a => a.Status == AssignedWorkAnswerStatus.NotSubmitted))
            {
                // Hide mentor-only fields for not submitted answers
                answer.MentorComment = null;
                answer.Score = null;
                answer.DetailedScore = null;
            }
        }

        return assignedWork;
    }

    public async Task<bool> IsMentorOwnWorkAsync(Ulid assignedWorkId, Ulid userId)
    {
        var assignedWork = await Context.Set<AssignedWorkModel>()
            .Where(aw => aw.Id == assignedWorkId)
            .FirstOrDefaultAsync();

        return assignedWork != null && (assignedWork.MainMentorId == userId || assignedWork.HelperMentorId == userId);
    }

    public async Task<bool> IsStudentOwnWorkAsync(Ulid assignedWorkId, Ulid userId)
    {
        var assignedWork = await Context.Set<AssignedWorkModel>()
            .Where(aw => aw.Id == assignedWorkId)
            .FirstOrDefaultAsync();

        return assignedWork != null && assignedWork.StudentId == userId;
    }

    public async Task<bool> IsWorkCheckStatusAsync(Ulid assignedWorkId, params AssignedWorkCheckStatus[] statuses)
    {
        var assignedWork = await Context.Set<AssignedWorkModel>()
            .Where(aw => aw.Id == assignedWorkId)
            .FirstOrDefaultAsync();

        return assignedWork != null && statuses.Contains(assignedWork.CheckStatus);
    }

    public async Task<bool> IsWorkSolveStatusAsync(Ulid assignedWorkId, params AssignedWorkSolveStatus[] statuses)
    {
        var assignedWork = await Context.Set<AssignedWorkModel>()
            .Where(aw => aw.Id == assignedWorkId)
            .FirstOrDefaultAsync();

        return assignedWork != null && statuses.Contains(assignedWork.SolveStatus);
    }
}


public static class IUnitOfWorkAssignedWorkRepositoryExtensions
{
    public static IAssignedWorkRepository AssignedWorkRepository(this IUnitOfWork unitOfWork)
    {
        return new AssignedWorkRepository()
        {
            Context = unitOfWork.Context,
        };
    }
}
