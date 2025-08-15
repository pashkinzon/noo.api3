using Noo.Api.AssignedWorks.Models;
using Noo.Api.Core.DataAbstraction.Db;

namespace Noo.Api.AssignedWorks.Services;

public class AssignedWorkHistoryService
{
    private readonly IAssignedWorkHistoryRepository _assignedWorkHistoryRepository;

    private readonly IUnitOfWork _unitOfWork;

    public AssignedWorkHistoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _assignedWorkHistoryRepository = unitOfWork.AssignedWorkHistoryRepository();
    }

    public Task<IEnumerable<AssignedWorkStatusHistoryModel>> GetHistoryAsync(Ulid assignedWorkId)
    {
        return _assignedWorkHistoryRepository.GetHistoryAsync(assignedWorkId);
    }

    public Task PushEventAsync(AssignedWorkStatusHistoryModel @event)
    {
        _assignedWorkHistoryRepository.Add(@event);
        return _unitOfWork.CommitAsync();
    }
}
