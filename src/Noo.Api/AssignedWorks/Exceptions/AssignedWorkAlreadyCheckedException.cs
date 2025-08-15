using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.AssignedWorks.Exceptions;

public class AssignedWorkAlreadyCheckedException : NooException
{
    public AssignedWorkAlreadyCheckedException()
        : base("The assigned work is already checked.")
    {
        Id = "ASSIGNED_WORK.ALREADY_CHECKED";
        StatusCode = HttpStatusCode.Conflict;
    }
}
