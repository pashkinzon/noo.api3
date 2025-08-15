using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.AssignedWorks.Exceptions;

public class AssignedWorkNotCheckedException : NooException
{
    public AssignedWorkNotCheckedException() : base("Assigned work is not checked.")
    {
        Id = "ASSIGNED_WORK.NOT_CHECKED";
        StatusCode = HttpStatusCode.Conflict;
    }
}
