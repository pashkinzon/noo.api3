namespace Noo.Api.AssignedWorks.Services;

public interface IAssignedWorkAccessService
{
    public Task<bool> CanGetAssignedWorkAsync(Ulid assignedWorkId);
    public Task<bool> CanSaveAssignedWorkAsync(Ulid assignedWorkId);
    public Task<bool> CanDeleteAssignedWorkAsync(Ulid assignedWorkId);
    public Task<bool> CanAddMainMentorAsync(Ulid assignedWorkId);
    public Task<bool> CanAddHelperMentorAsync(Ulid assignedWorkId);
}
