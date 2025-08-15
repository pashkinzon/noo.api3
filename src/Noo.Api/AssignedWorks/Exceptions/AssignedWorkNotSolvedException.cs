using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.AssignedWorks.Exceptions;

public class AssignedWorkNotSolvedException : NooException
{
    public AssignedWorkNotSolvedException() : base("Assigned work is not solved.")
    {
        Id = "ASSIGNED_WORK.NOT_SOLVED";
        StatusCode = HttpStatusCode.Conflict;
    }
}
