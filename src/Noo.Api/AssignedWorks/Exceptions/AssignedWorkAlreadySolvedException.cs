using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.AssignedWorks.Exceptions;

public class AssignedWorkAlreadySolvedException : NooException
{
    public AssignedWorkAlreadySolvedException()
        : base("The assigned work is already solved.")
    {
        Id = "ASSIGNED_WORK.ALREADY_SOLVED";
        StatusCode = HttpStatusCode.Conflict;
    }
}
