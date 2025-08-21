namespace Noo.Api.AssignedWorks.Types;

public enum AssignedWorkStatusHistoryType
{
    StartedSolving,
    SolveDeadlineShifted,
    Solved,
    StartedChecking,
    CheckDeadlineShifted,
    Checked,
    SentOnRecheck,
    SentOnResolve,
    HelperMentorAdded,
    HelperMentorRemoved,
    MainMentorChanged
}
