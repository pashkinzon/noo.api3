namespace Noo.Api.AssignedWorks.Models;

public static class AssignedWorkEnumDbDataTypes
{
    public const string AssignedWorkStatusHistoryType = "ENUM('StartedSolving', 'SolveDeadlineShifted', 'Solved', 'StartedChecking', 'CheckDeadlineShifted', 'Checked', 'SentOnRecheck', 'SentOnResolve')";

    public const string AssignedWorkSolveStatus = "ENUM('NotSolved', 'InProgress', 'Solved')";

    public const string AssignedWorkCheckStatus = "ENUM('NotChecked', 'InProgress', 'Checked')";
}
