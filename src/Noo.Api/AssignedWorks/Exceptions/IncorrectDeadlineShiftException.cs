using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.AssignedWorks.Exceptions;

public class IncorrectDeadlineShiftException : NooException
{
    public IncorrectDeadlineShiftException()
        : base("The deadline shift is incorrect.")
    {
        Id = "ASSIGNED_WORK.INCORRECT_DEADLINE_SHIFT";
        StatusCode = HttpStatusCode.BadRequest;
    }
}
