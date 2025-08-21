namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkLinkGenerator
{
    public string GenerateViewLink(Ulid assignedWorkId);
}
