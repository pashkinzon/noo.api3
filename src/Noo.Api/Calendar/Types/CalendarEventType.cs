namespace Noo.Api.Calendar.Types;

public enum CalendarEventType
{
    /// <summary>
    /// Event type when the user adds it manually.
    /// </summary>
    Custom,

    /// <summary>
    /// Check Deadline event type for assigned works
    /// </summary>
    AssignedWorkCheckDeadline,

    /// <summary>
    /// Solve Deadline event type for assigned works
    /// </summary>
    AssignedWorkSolveDeadline,

    /// <summary>
    /// Event that tells that an assigned work is checked
    /// </summary>
    AssignedWorkCheked,

    /// <summary>
    /// Event that tells that an assigned work is made
    /// </summary>
    AssignedWorkSolved,
}
