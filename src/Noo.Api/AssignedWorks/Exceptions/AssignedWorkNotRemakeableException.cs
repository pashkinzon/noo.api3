using System.Net;
using Noo.Api.Core.Exceptions;

namespace Noo.Api.AssignedWorks.Exceptions;

public class AssignedWorkNotRemakeableException : NooException
{
    public AssignedWorkNotRemakeableException() : base("Assigned work is not remakeable.")
    {
        Id = "ASSIGNED_WORK.NOT_REMAKEABLE";
        StatusCode = HttpStatusCode.BadRequest;
    }
}
